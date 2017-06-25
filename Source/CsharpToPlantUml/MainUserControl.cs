using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CodeToUml
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }

        public void OnLoad()
        {
            // コントロールを、フォームに合わせて広げます
            textBox1.SetBounds(0, textBox1.Bounds.Y, Width, textBox1.Height);
            translationButton.SetBounds(0, translationButton.Bounds.Y, Width, translationButton.Height);
            textBox2.SetBounds(0, textBox2.Bounds.Y, Width, textBox2.Height);
        }

        /// <summary>
        /// ダンプ
        /// </summary>
        static void Dump(string[] tokens)
        {
            int i = 0;
            foreach (string token in tokens)
            {
                Trace.WriteLine("(" + i + ")[" + token+"]");
                i++;
            }
        }

        /// <summary>
        /// ダンプ
        /// </summary>
        static void Dump(List<string> tokens)
        {
            int i = 0;
            foreach (string token in tokens)
            {
                Trace.WriteLine("(" + i + ")[" + token+"]");
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
        static string[] LexicalParse1(string text)
        {
            return text.Split(' ');
        }

        static void FlushWord(StringBuilder word, List<string> tokens2)
        {
            if (0 < word.Length) { tokens2.Add(word.ToString()); word.Clear(); }
        }
        /// <summary>
        /// 字句解析2
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        static List<string> LexicalParse2(string[] tokens)
        {
            StringBuilder word = new StringBuilder();

            List<string> tokens2 = new List<string>();
            foreach (string token in tokens)
            {
                int caret = 0;
                while (caret< token.Length)
                {
                    switch (token[caret])
                    {
                        case '\r':
                            {
                                if (caret+1 < token.Length && '\n' ==token[caret+1])
                                {
                                    // 改行
                                    FlushWord(word, tokens2);
                                    tokens2.Add(NEWLINE);// 改行は'\n'１つに変換
                                    caret+=2;
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
                                        caret+=3;
                                    }
                                    else
                                    {
                                        // 「//」
                                        FlushWord(word, tokens2);
                                        tokens2.Add("//");
                                        caret+=2;
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
        static List<string> LexicalParse3(List<string> tokens)
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

        /// <summary>
        /// [変換]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translationButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            // 例
            // static Score score_cache;

            #region レキサー
            List<string> tokens;
            {
                Trace.WriteLine("フェーズ1");
                string[] tokens1 = LexicalParse1(textBox1.Text);
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

            bool isLineComment = false;//一行コメント
            bool isSummaryComment = false;//<summary>～</summary>
            StringBuilder comment = new StringBuilder();

            bool isStatic = false;//修飾子
            AccessModify accessModify = AccessModify.Private; // 記述が無ければプライベート
            bool readType = false;
            string type = "";
            bool readName = false;
            string name = "";
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
                    switch (token)
                    {
                        case "///": isLineComment = true; break;
                        case "//": isLineComment = true; break;
                        case "static": isStatic = true; break;
                        default:
                            {
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
            }
            #endregion

            #region ビルダー
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
            // 名前
            sb.Append(name);
            sb.Append(" : ");
            // 型
            sb.Append(type);
            if (0<comment.Length)
            {
                sb.Append(" : '");
                sb.Append(comment.ToString());
                sb.Append("'");
            }
            #endregion

            textBox2.Text = sb.ToString();
        }
    }
}
