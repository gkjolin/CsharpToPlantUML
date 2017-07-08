using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpToPlantUml;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProperty()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        static Type propertyName;
";
            string expected = @"{static} - propertyName : Type 'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        /// <summary>
        /// 複数行ドキュメントコメント
        /// </summary>
        [TestMethod]
        public void TestMultipleDocumentComment()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント１行目
        /// ドキュメント・コメント２行目
        /// ドキュメント・コメント３行目
        /// </summary>
        static Type propertyName;
";
            string expected = @"{static} - propertyName : Type 'ドキュメント・コメント１行目 ドキュメント・コメント２行目 ドキュメント・コメント３行目'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestConstProperty()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        const string CONST_STRING_NAME = ""This is a value."";

";
            string expected = @"- const CONST_STRING_NAME : string 'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        /// <summary>
        /// アトリビュートを無視
        /// </summary>
        [TestMethod]
        public void TestConstPropertyIgnoreAttribute()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        [Tooltip(""画像ファイル名"")]
        public string propertyName = ""This is a value."";

";
            string expected = @"+ propertyName : string 'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestConstructor()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        ConstructorName()

";
            string expected = @"- ConstructorName() :  'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestMethod()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        /// <returns></returns>
        public static Type MethodName()
";
            string expected = @"{static} + MethodName() : Type 'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        /// <summary>
        /// 引数付きメソッド
        /// </summary>
        [TestMethod]
        public void TestMethodWithArgumentList()
        {
            string input = @"        /// <summary>
        /// ドキュメント・コメント
        /// </summary>
        /// <returns></returns>
        public static Type MethodName(int a,int b)
";
            string expected = @"{static} + MethodName(int a,int b) : Type 'ドキュメント・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }
    }
}
