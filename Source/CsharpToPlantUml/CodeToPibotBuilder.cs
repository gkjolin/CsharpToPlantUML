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
    public class CodeToPibotBuilder
    {
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
                                    tokens2.Add(Common.NEWLINE);// 改行は'\n'１つに変換
                                    caret += 2;
                                }
                                else
                                {
                                    word.Append(token[caret]);
                                    caret++;
                                }
                            }
                            break;
                        case '['://thru
                        case ']'://thru
                        case '('://thru
                        case ')'://thru
                        case ';':
                            {
                                // 1文字で分けるもの
                                FlushWord(word, tokens2);
                                tokens2.Add(token[caret].ToString());
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

        #region 分類器
        bool isLineComment = false;//一行コメント
        bool isSummaryComment = false;//<summary>～</summary>

        bool isAttribute = false;//例：[Tooltip("画像ファイル名")]

        bool startedSigunature = false;
        bool endMofify = false;

        bool readType = false;
        bool readName = false;

        bool isArgumentList = false;
        /// <summary>
        /// 分類器
        /// </summary>
        void Classificate(List<string> tokens, Pibot pibot)
        {
            foreach (string token in tokens)
            {
                if (!startedSigunature)
                {
                    if (pibot.isStatic || pibot.isConst || pibot.accessModify != Pibot.AccessModify.Private || readType || readName)
                    {
                        startedSigunature = true;
                    }
                }

                if (isLineComment)
                {
                    switch (token)
                    {
                        case Common.NEWLINE: pibot.comment.Append(" "); isLineComment = false; break;
                        case "<summary>": isSummaryComment = true; break;
                        case "</summary>": isSummaryComment = false; break;
                        default:
                            {
                                if (isSummaryComment)
                                {
                                    if ("\n"==token)
                                    {
                                        // 改行は 半角スペースに変換して１行にする
                                        pibot.comment.Append(" ");
                                    }
                                    else
                                    {
                                        pibot.comment.Append(token);
                                    }
                                }
                                else
                                {
                                    // 無視
                                }
                            }
                            break;
                    }
                }
                else if (isAttribute)
                {
                    switch (token)
                    {
                        case "]": isAttribute = false; break;
                        default: break;// 無視
                    }
                }
                else if (isArgumentList)
                {
                    switch (token)
                    {
                        case ")":
                            {
                                // 最後の空白は消しておく
                                string temp = pibot.argumentList.ToString().TrimEnd();
                                pibot.argumentList.Clear();
                                pibot.argumentList.Append(temp);
                                pibot.argumentList.Append(token);
                                isArgumentList = false;
                            }
                            break;
                        default:
                            {
                                pibot.argumentList.Append(token);
                                // 引数の型と名前を区切る空白が無くなっているので、
                                // 適当に足しておく
                                pibot.argumentList.Append(" ");
                            }
                            break;
                    }
                }
                else
                {
                    switch (token)
                    {
                        case "///": isLineComment = true; break;
                        case "//": isLineComment = true; break;
                        case "\n": break;
                        default:
                            {
                                if (!startedSigunature && "["==token)
                                {
                                    isAttribute = true;
                                }
                                else
                                {
                                    if (!endMofify)
                                    {
                                        switch (token)
                                        {
                                            case "static": pibot.isStatic = true; goto gt_next;
                                            case "const": pibot.isConst = true; goto gt_next;
                                            case "public": pibot.accessModify = Pibot.AccessModify.Public; goto gt_next;
                                        }
                                        endMofify = true;
                                    }

                                    if (!readType)
                                    {
                                        pibot.type = token;
                                        readType = true;
                                    }
                                    else if ("(" == token) // コンストラクタの場合は名前より早く ( が来る
                                    {
                                        if (!isArgumentList)
                                        {
                                            pibot.argumentList.Append(token);
                                            isArgumentList = true;
                                        }
                                    }
                                    else if (!readName)
                                    {
                                        pibot.name = token;
                                        readName = true;
                                    }
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



        public Pibot Translate(string text1)
        {
            #region レキサー
            List<string> tokens;
            {
                Trace.WriteLine("フェーズ1");
                string[] tokens1 = LexicalParse1(text1);
                // ダンプ
                Common.Dump(tokens1);

                Trace.WriteLine("フェーズ2");
                List<string> tokens2 = LexicalParse2(tokens1);
                // ダンプ
                Common.Dump(tokens2);

                Trace.WriteLine("フェーズ3");
                tokens = Common.DeleteEmptyStringElement(tokens2);
                // ダンプ
                Common.Dump(tokens);
            }
            #endregion

            #region クラシフィケーション
            Pibot pibot = new Pibot();
            Classificate(tokens, pibot);
            #endregion

            return pibot;
        }

    }
}
