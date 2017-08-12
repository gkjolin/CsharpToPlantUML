using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpToPlantUml;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 複数行サマリーコメント
        /// </summary>
        [TestMethod]
        public void TestCommentSummaryMultiple()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント１行目
        /// サマリー・コメント２行目
        /// サマリー・コメント３行目
        /// </summary>
        static Type propertyName;
";
            string expected = @"- propertyName : Type {static} 'サマリー・コメント１行目 サマリー・コメント２行目 サマリー・コメント３行目'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestProperty()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        static Type propertyName;
";
            string expected = @"- propertyName : Type {static} 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestPropertyArray()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        Type[] propertyName;
";
            string expected = @"- propertyName : Type[] 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestPropertyArrayArray()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        Type[][] propertyName;
";
            string expected = @"- propertyName : Type[][] 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestPropertyGenerictype()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        Dictionary<Type1, Type2> propertyName;
";
            string expected = @"- propertyName : Dictionary<Type1, Type2> 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestPropertyConst()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        const string CONST_STRING_NAME = ""This is a value."";

";
            string expected = @"- CONST_STRING_NAME : string const 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestPropertyReadonly()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        readonly string CONST_STRING_NAME = ""This is a value."";

";
            string expected = @"- CONST_STRING_NAME : string <<readonly>> 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        /// <summary>
        /// アトリビュートを無視
        /// </summary>
        [TestMethod]
        public void TestPropertyConstIgnoreAttribute()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        [Tooltip(""画像ファイル名"")]
        public string propertyName = ""This is a value."";

";
            string expected = @"+ propertyName : string 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestConstructor()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        ConstructorName()

";
            string expected = @"- ConstructorName() :  'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestMethod()
        {
            string input = @"        /// <summary>
        /// サマリー・コメント
        /// </summary>
        /// <returns></returns>
        public static Type MethodName()
";
            string expected = @"+ MethodName() : Type {static} 'サマリー・コメント'";

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
        /// サマリー・コメント
        /// </summary>
        /// <returns></returns>
        public static Type MethodName(int a,int b)
";
            string expected = @"+ MethodName(int a,int b) : Type {static} 'サマリー・コメント'";

            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(input);
            string output = new PibotToUmlBuilder().Build(pibot);

            Assert.AreEqual(expected: expected, actual: output);
        }
    }
}
