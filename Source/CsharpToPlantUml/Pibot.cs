using System.Text;

namespace CsharpToPlantUml
{
    public class Pibot
    {
        public Pibot()
        {
            // 記述が無ければプライベート
            accessModify = AccessModify.Private;
            name = "";
            type = "";
            genericParameters = new StringBuilder();
            argumentList = new StringBuilder();
            documentComment = new StringBuilder();
            summaryComment = new StringBuilder();
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
        /// ジェネリック型引数
        /// 例：「Type1, Type2」
        /// </summary>
        public StringBuilder genericParameters;

        /// <summary>
        /// 引数のリスト
        /// </summary>
        public StringBuilder argumentList;

        /// <summary>
        /// ドキュメント・コメント全体
        /// </summary>
        public StringBuilder documentComment;

        /// <summary>
        /// サマリー・コメント
        /// </summary>
        public StringBuilder summaryComment;

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
