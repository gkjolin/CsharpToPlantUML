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
            string[] tokens = textBox1.Text.Split(' ');

            // ダンプ
            Dump(tokens);

            List<string> tokens2 = new List<string>();
            foreach (string token in tokens)
            {
                if (""==token)
                {
                    // 空文字列は無視
                }
                else if (token.Contains(Environment.NewLine))
                {
                    // 改行（\r\nの２文字）は分ける
                    int i = token.IndexOf(Environment.NewLine);
                    //Debug.WriteLine("token=[" + token + "] token.Length=" + token.Length + " Environment.NewLine.Length=" + Environment.NewLine.Length+" i="+i);
                    tokens2.Add(token.Substring(0, i));
                    tokens2.Add(NEWLINE); // Substring()は改行を数えてくれない？
                    tokens2.Add(token.Substring(i + Environment.NewLine.Length));
                }
                else if (token.Contains(';'))
                {
                    // セミコロンは分ける
                    int i = token.IndexOf(';');
                    tokens2.Add(token.Substring(0, i));
                    tokens2.Add(token.Substring(i));
                }
                else if (token.Contains("///"))
                {
                    // 複数行コメントは分ける
                    int i = token.IndexOf("///");
                    tokens2.Add(token.Substring(0, i));
                    tokens2.Add(token.Substring(i, i + "///".Length));
                    tokens2.Add(token.Substring(i + "///".Length));
                }
                else if (token.Contains("//"))
                {
                    // 複数行コメントは分ける
                    int i = token.IndexOf("//");
                    tokens2.Add(token.Substring(0, i));
                    tokens2.Add(token.Substring(i, i+"//".Length));
                    tokens2.Add(token.Substring(i + "//".Length));
                }
                else
                {
                    tokens2.Add(token);
                }
            }

            // ダンプ
            Dump(tokens2);

            // 空文字列を除去
            List<string> tokens3 = new List<string>();
            foreach (string token in tokens2)
            {
                if ("" == token)
                {
                    // 空文字列は無視
                }
                else
                {
                    tokens3.Add(token);
                }
            }

            // ダンプ
            Dump(tokens3);
            #endregion

            #region クラシフィケーション
            bool isLineComment = false;//一行コメント
            StringBuilder comment = new StringBuilder();
            bool isStatic = false;//修飾子
            AccessModify accessModify = AccessModify.Private; // 記述が無ければプライベート
            bool readType = false;
            string type = "";
            bool readName = false;
            string name = "";
            foreach (string token in tokens3)
            {
                if (isLineComment)
                {
                    switch (token)
                    {
                        case NEWLINE: comment.Append(" "); isLineComment = false; break;
                        default: comment.Append(token); break;
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
