using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpToPlantUml
{
    public class UmlToPibotBuilder
    {
        public bool isDeleteFontTag;


        #region レキサー
        /// <summary>
        /// 字句解析1
        /// 半角空白で分割
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string[] LexicalParse1(string text)
        {
            return text.Split(' ');
        }

        /// <summary>
        /// バッファー・フラッシュ
        /// </summary>
        /// <param name="word"></param>
        /// <param name="tokens2"></param>
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
                        // 「//」といったコメントは使わないだろう
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
        #endregion


        #region 分類器
        /// <summary>
        /// 100: start
        /// </summary>
        int phase;
        /// <summary>
        /// 分類器
        /// </summary>
        void Classificate(List<string> tokens, Pibot pibot)
        {
            pibot.accessModify = Pibot.AccessModify.Private;
            phase = 100;

            foreach (string token in tokens)
            {
                bool parsed = false;

                // 修飾子
                switch (token)
                {
                    case "+":
                        pibot.accessModify = Pibot.AccessModify.Public;
                        phase = 200;
                        parsed = true;
                        break;
                }

                // 要素名
                if (!parsed && phase < 300)
                {
                    pibot.name = token;
                    phase = 300;
                    parsed = true;
                }

                // 「(」
                if (!parsed && phase < 400 && token == "(")
                {
                    pibot.name = token;
                    phase = 400;
                    parsed = true;
                }

                // 「)」
                if (!parsed && phase < 500 && token == ")")
                {
                    pibot.name = token;
                    phase = 500;
                    parsed = true;
                }

                // 「:」
                if (!parsed && phase < 600 && token == ":")
                {
                    pibot.name = token;
                    phase = 600;
                    parsed = true;
                }

                // 型名
                if (!parsed && phase < 700)
                {
                    pibot.name = token;
                    phase = 700;
                    parsed = true;
                }

                // 「'」
                if (!parsed && phase < 800 && token == "'")
                {
                    pibot.name = token;
                    phase = 800;
                    parsed = true;
                }

                // これ以降、コメント
                pibot.comment.Append(token);
            }


        }
        #endregion


        public Pibot Build(string text)
        {
            #region レキサー
            string[] tokens1 = LexicalParse1(text);
            Common.Dump(tokens1);

            List<string> tokens2 = LexicalParse2(tokens1);
            Common.Dump(tokens2);

            tokens2 = Common.DeleteEmptyStringElement(tokens2);
            Common.Dump(tokens2);
            #endregion

            Pibot pibot = new Pibot();
            Classificate(tokens2, pibot);

            return pibot;
        }
    }
}
