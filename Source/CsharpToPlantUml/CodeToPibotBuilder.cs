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
        /// 字句解析(Lexical Parser)
        /// 半角スペースでの分割
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string[] Lex_SplitBySpace(string text)
        {
            return text.Split(' ');
        }

        void FlushWord(StringBuilder word, List<string> tokens2)
        {
            if (0 < word.Length) { tokens2.Add(word.ToString()); word.Clear(); }
        }
        /// <summary>
        /// 字句解析(Lexical Parser)
        /// 
        /// 字トークンへの分割
        /// </summary>
        /// <param name="words">半角空白で区切った程度のトークン</param>
        /// <returns></returns>
        List<string> Lex_ToCharChanks(string[] words)
        {
            StringBuilder chars = new StringBuilder();

            List<string> tokens2 = new List<string>();
            foreach (string word in words)
            {
                int caret = 0;
                while (caret < word.Length)
                {
                    switch (word[caret])
                    {
                        case '\r':
                            {
                                if (caret + 1 < word.Length && '\n' == word[caret + 1])
                                {
                                    // 改行
                                    FlushWord(chars, tokens2);
                                    tokens2.Add(Common.NEWLINE);// 改行は'\n'１つに変換
                                    caret += 2;
                                }
                                else
                                {
                                    chars.Append(word[caret]);
                                    caret++;
                                }
                            }
                            break;
                        case '['://thru
                        case ']'://thru
                        case '('://thru
                        case ')'://thru
                        case '<'://thru
                        case '>'://thru
                        case ','://thru
                        case ';':
                            {
                                // 1文字で分けるもの
                                FlushWord(chars, tokens2);
                                tokens2.Add(word[caret].ToString());
                                caret++;
                            }
                            break;
                        case '/':
                            {
                                if (caret + 1 < word.Length && '/' == word[caret + 1])
                                {
                                    if (caret + 2 < word.Length && '/' == word[caret + 2])
                                    {
                                        // 「///」
                                        FlushWord(chars, tokens2);
                                        tokens2.Add("///");
                                        caret += 3;
                                    }
                                    else
                                    {
                                        // 「//」
                                        FlushWord(chars, tokens2);
                                        tokens2.Add("//");
                                        caret += 2;
                                    }
                                }
                                else
                                {
                                    chars.Append(word[caret]);
                                    caret++;
                                }
                            }
                            break;
                        default:
                            chars.Append(word[caret]);
                            caret++;
                            break;
                    }
                }

                FlushWord(chars, tokens2);
            }

            return tokens2;
        }

        #region 分類器
        bool inSinglelineComment = false; // 一行コメント中
        bool inDocumentComment = false; // ドキュメント・コメント中
        bool inSummaryComment = false; // < summary >～< /summary >

        bool isAttribute = false;//例：[Tooltip("画像ファイル名")]
        int depthOfOpendAngleBracketForGenericParameters = int.MaxValue;//例：<Type1,Type2>
        int depthOfOpendAngleBracket = 0;//例：<

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
                    if (pibot.isStatic ||
                        pibot.isConst ||
                        pibot.isOverride ||
                        pibot.isReadonly ||
                        pibot.isVirtual ||
                        pibot.accessModify != Pibot.AccessModify.Private ||
                        readType ||
                        readName
                        )
                    {
                        startedSigunature = true;
                    }
                }

                #region ドキュメント・コメント
                if (inDocumentComment)
                {
                    pibot.documentComment.Append(token);

                    if (">"==token)
                    {
                        string docCmt = pibot.documentComment.ToString();
                        if (docCmt.EndsWith("<summary>"))
                        {
                            inSummaryComment = true;
                            goto gt_EndLineComment;
                        }
                        else if (docCmt.EndsWith("</summary>"))
                        {
                            pibot.summaryComment.Length -= "</summary>".Length;
                            inSummaryComment = false;
                            goto gt_EndLineComment;
                        }
                    }

                    switch (token)
                    {
                        case Common.NEWLINE: pibot.summaryComment.Append(" "); inDocumentComment = false; break;
                        default:
                            {
                                if (inSummaryComment)
                                {
                                    if ("\n"==token)
                                    {
                                        // 改行は 半角スペースに変換して１行にする
                                        pibot.summaryComment.Append(" ");
                                    }
                                    else
                                    {
                                        pibot.summaryComment.Append(token);
                                    }
                                }
                            }
                            break;
                    }

                    gt_EndLineComment:
                    ;
                }
                #endregion
                #region 一行コメント
                else if (inSinglelineComment)
                {
                    if ("\n" == token)
                    {
                        inSinglelineComment = false;
                    }
                }
                #endregion
                #region アトリビュート
                else if (isAttribute)
                {
                    switch (token)
                    {
                        case "]": isAttribute = false; break;
                        default: break;// 無視
                    }
                }
                #endregion
                #region ジェネリック引数
                else if (depthOfOpendAngleBracketForGenericParameters <= depthOfOpendAngleBracket)
                {
                    switch (token)
                    {
                        case ",":
                            {
                                // カンマの後ろに空白を足しておく
                                pibot.genericParameters.Append(token);
                                pibot.genericParameters.Append(" ");
                            }
                            break;
                        case ">":
                            {
                                pibot.genericParameters.Append(token);
                                depthOfOpendAngleBracket--;
                            }
                            break;
                        default:
                            {
                                pibot.genericParameters.Append(token);
                            }
                            break;
                    }
                }
                #endregion
                #region 引数のリスト
                else if (isArgumentList)
                {
                    switch (token)
                    {
                        // 直前の空白は削除して追加したい
                        case ",": // thru
                        case ")":
                            {
                                // 消す前に退避
                                string temp = pibot.argumentList.ToString().TrimEnd();
                                // 最後の空白は消しておく
                                pibot.argumentList.Clear();
                                pibot.argumentList.Append(temp);
                                pibot.argumentList.Append(token);
                                if (")"== token)
                                {
                                    isArgumentList = false;
                                }
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
                #endregion
                else
                {
                    switch (token)
                    {
                        case "///": inDocumentComment = true; break;
                        case "//": inSinglelineComment = true; break;
                        case "\n": break;
                        default:
                            {
                                if (!startedSigunature && "[" == token)
                                {
                                    isAttribute = true;
                                }
                                else
                                {
                                    if (!endMofify)
                                    {
                                        switch (token)
                                        {
                                            case "const": pibot.isConst = true; goto gt_next;
                                            case "internal": pibot.accessModify = Pibot.AccessModify.Internal; goto gt_next;
                                            case "override": pibot.isOverride = true; goto gt_next;
                                            case "private": pibot.accessModify = Pibot.AccessModify.Private; goto gt_next;
                                            case "protected": pibot.accessModify = Pibot.AccessModify.Protected; goto gt_next;
                                            case "protected internal": pibot.accessModify = Pibot.AccessModify.ProtectedInternal; goto gt_next;// not working. FIXME: トークンにしたい
                                            case "public": pibot.accessModify = Pibot.AccessModify.Public; goto gt_next;
                                            case "readonly": pibot.isReadonly = true; goto gt_next;
                                            case "static": pibot.isStatic = true; goto gt_next;
                                            case "virtual": pibot.isVirtual = true; goto gt_next;
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
                                    else if ("<" == token)
                                    {
                                        pibot.genericParameters.Append(token);
                                        depthOfOpendAngleBracket++;
                                        if (!readName)
                                        {
                                            depthOfOpendAngleBracketForGenericParameters = depthOfOpendAngleBracket;
                                        }
                                    }
                                    else if (">" == token)
                                    {
                                        pibot.genericParameters.Append(token);
                                        depthOfOpendAngleBracket--;
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
                string[] tokens1 = Lex_SplitBySpace(text1);
                // ダンプ
                Common.Dump(tokens1);

                Trace.WriteLine("フェーズ2");
                List<string> tokens2 = Lex_ToCharChanks(tokens1);
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
