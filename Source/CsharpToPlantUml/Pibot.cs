using System.Text;

namespace CsharpToPlantUml
{
    /// <summary>
    /// 参考：「(補足) Roslyn で C#のソースコードからPlantUMLのクラス図を生成する の設計メモ」http://pierre3.hatenablog.com/entry/2015/09/11/234924
    /// </summary>
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
        /// 修飾子
        /// </summary>
        public bool isReadonly;

        /// <summary>
        /// 修飾子
        /// </summary>
        public bool isVirtual;

        /// <summary>
        /// 修飾子
        /// </summary>
        public bool isOverride;

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
            Internal,
            ProtectedInternal,
            Protected,
            Private,
            Num
        }

    }
}
