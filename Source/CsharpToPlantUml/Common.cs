using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CsharpToPlantUml
{
    public static class Common
    {
        /// <summary>
        /// 改行変数 "\r\n" 他を文字列定数 "\n" に統一する
        /// </summary>
        public const string NEWLINE = "\n";

        /// <summary>
        /// ダンプ
        /// </summary>
        public static void Dump(string[] tokens)
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
        public static void Dump(List<string> tokens)
        {
            int i = 0;
            foreach (string token in tokens)
            {
                Trace.WriteLine("(" + i + ")[" + token + "]");
                i++;
            }
        }

        /// <summary>
        /// 字句解析3
        /// 空エレメントを除去
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static List<string> DeleteEmptyStringElement(List<string> tokens)
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

    }
}
