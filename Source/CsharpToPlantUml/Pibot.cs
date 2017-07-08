using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpToPlantUml
{
    public class Pibot
    {
        public Pibot()
        {
            // 記述が無ければプライベート
            accessModify = Pibot.AccessModify.Private;
            name = "";
            type = "";
            argumentList = new StringBuilder();
            comment = new StringBuilder();
        }

        /// <summary>
        /// 修飾子
        /// </summary>
        public bool isStatic;

        /// <summary>
        /// 修飾子
        /// </summary>
        public bool isConst;

        /// <summary>
        /// 要素名
        /// </summary>
        public string name;

        /// <summary>
        /// 型名
        /// </summary>
        public string type;

        /// <summary>
        /// 引数のリスト
        /// </summary>
        public StringBuilder argumentList;

        /// <summary>
        /// コメント
        /// </summary>
        public StringBuilder comment;

        /// <summary>
        /// アクセス修飾子
        /// </summary>
        public AccessModify accessModify;

        /// <summary>
        /// アクセス修飾子
        /// </summary>
        public enum AccessModify
        {
            Public,
            Private,
            Num
        }

    }
}
