using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CsharpToPlantUml
{
    /// <summary>
    /// 翻訳機
    /// </summary>
    public class Translator
    {
        /// <summary>
        /// ダンプ
        /// </summary>
        void Dump(string[] tokens)
        {
            int i = 0;
            foreach (string token in tokens)
            {
                Trace.WriteLine("(" + i + ")[" + token + "]");
                i++;
            }
        }

        /// <summary>
        /// ダンプ
        /// </summary>
        void Dump(List<string> tokens)
        {
            int i = 0;
            foreach (string token in tokens)
            {
                Trace.WriteLine("(" + i + ")[" + token + "]");
                i++;
            }
        }

        /// <summary>
        /// アクセス修飾子
        /// </summary>
        enum AccessModify
        {
            Public,
            Private,
            Num
        }

        /// <summary>
        /// 改行変数 "\r\n" 他を文字列定数 "\n" に統一する
        /// </summary>
        const string NEWLINE = "\n";

        /// <summary>
        /// 字句解析1
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string[] LexicalParse1(string text)
        {
            return text.Split(' ');
        }

        void FlushWord(StringBuilder word, List<string> tokens2)
        {
            if (0 < word.Length) { tokens2.Add(word.ToString()); word.Clear(); }
        }
        /// <summary>
        /// 字句解析2
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        List<string> LexicalParse2(string[] tokens)
        {
            StringBuilder word = new StringBuilder();

            List<string> tokens2 = new List<string>();
            foreach (string token in tokens)
            {
                int caret = 0;
                while (caret < token.Length)
                {
                    switch (token[caret])
                    {
                        case '\r':
                            {
                                if (caret + 1 < token.Length && '\n' == token[caret + 1])
                                {
                                    // 改行
                                    FlushWord(word, tokens2);
                                    tokens2.Add(NEWLINE);// 改行は'\n'１つに変換
                                    caret += 2;
                                }
                                else
                                {
                                    word.Append(token[caret]);
                                    caret++;
                                }
                            }
                            break;
                        case ';':
                            {
                                FlushWord(word, tokens2);
                                tokens2.Add(token[caret].ToString());// セミコロンは分ける
                                caret++;
                            }
                            break;
                        case '/':
                            {
                                if (caret + 1 < token.Length && '/' == token[caret + 1])
                                {
                                    if (caret + 2 < token.Length && '/' == token[caret + 2])
                                    {
                                        // 「///」
                                        FlushWord(word, tokens2);
                                        tokens2.Add("///");
                                        caret += 3;
                                    }
                                    else
                                    {
                                        // 「//」
                                        FlushWord(word, tokens2);
                                        tokens2.Add("//");
                                        caret += 2;
                                    }
                                }
                                else
                                {
                                    word.Append(token[caret]);
                                    caret++;
                                }
                            }
                            break;
                        default:
                            word.Append(token[caret]);
                            caret++;
                            break;
                    }
                }

                FlushWord(word, tokens2);
            }

            return tokens2;
        }

        /// <summary>
        /// 字句解析3
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        List<string> LexicalParse3(List<string> tokens)
        {
            // 空文字列を除去
            List<string> tokens2 = new List<string>();
            foreach (string token in tokens)
            {
                if ("" == token)
                {
                    // 空文字列は無視
                }
                else
                {
                    tokens2.Add(token);
                }
            }

            return tokens2;
        }

        #region 分類器
        bool isLineComment = false;//一行コメント
        bool isSummaryComment = false;//<summary>～</summary>
        StringBuilder comment = new StringBuilder();

        bool isStatic = false;//修飾子
        bool isConst = false;//修飾子
        AccessModify accessModify = AccessModify.Private; // 記述が無ければプライベート
        bool endMofify = false;

        bool readType = false;
        string type = "";
        bool readName = false;
        string name = "";
        /// <summary>
        /// 分類器
        /// </summary>
        void Classificate(List<string> tokens)
        {
            foreach (string token in tokens)
            {
                if (isLineComment)
                {
                    switch (token)
                    {
                        case NEWLINE: comment.Append(" "); isLineComment = false; break;
                        case "<summary>": isSummaryComment = true; break;
                        case "</summary>": isSummaryComment = false; break;
                        default:
                            {
                                if (isSummaryComment)
                                {
                                    comment.Append(token);
                                }
                                else
                                {
                                    // 無視
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (!endMofify)
                    {

                    }

                    switch (token)
                    {
                        case "///": isLineComment = true; break;
                        case "//": isLineComment = true; break;
                        default:
                            {
                                if (!endMofify)
                                {
                                    switch (token)
                                    {
                                        case "static": isStatic = true; goto gt_next;
                                        case "const": isConst = true; goto gt_next;
                                        case "public": accessModify = AccessModify.Public; goto gt_next;
                                    }
                                    endMofify = true;
                                }

                                if (!readType)
                                {
                                    type = token;
                                    readType = true;
                                }
                                else if (!readName)
                                {
                                    name = token;
                                    readName = true;
                                }
                            }
                            break;
                    }
                }

                gt_next:
                ;
            }
        }
        #endregion

        public string Build()
        {
            StringBuilder sb = new StringBuilder();

            // 修飾子
            if (isStatic)
            {
                sb.Append("{static} ");
            }
            // アクセス修飾子
            switch (accessModify)
            {
                case AccessModify.Private: sb.Append("- "); break;
                case AccessModify.Public: sb.Append("+ "); break;
            }
            // 修飾子
            if (isConst)
            {
                sb.Append("const ");
            }
            // 名前
            sb.Append(name);
            sb.Append(" : ");
            // 型
            sb.Append(type);
            if (0 < comment.Length)
            {
                sb.Append(" '");
                sb.Append(comment.ToString().Trim());
                sb.Append("'");
            }

            return sb.ToString();
        }

        public string Translate(string text1)
        {
            #region レキサー
            List<string> tokens;
            {
                Trace.WriteLine("フェーズ1");
                string[] tokens1 = LexicalParse1(text1);
                // ダンプ
                Dump(tokens1);

                Trace.WriteLine("フェーズ2");
                List<string> tokens2 = LexicalParse2(tokens1);
                // ダンプ
                Dump(tokens2);

                Trace.WriteLine("フェーズ3");
                tokens = LexicalParse3(tokens2);
                // ダンプ
                Dump(tokens);
            }
            #endregion

            #region クラシフィケーション
            Classificate(tokens);
            #endregion

            return Build();
        }

    }
}
