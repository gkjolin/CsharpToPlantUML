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
        /// スコアのスクリプトのキャッシュ
        /// </summary>
        static Score score_cache;
";
            string expected = @"{static} - score_cache : Score 'スコアのスクリプトのキャッシュ'";

            Translator translator = new Translator();
            string output = translator.Translate(input);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestConstProperty()
        {
            string input = @"        /// <summary>
        /// ゲームオブジェクト名
        /// </summary>
        const string PREFABS_ROOT = ""Prefabs Root"";

";
            string expected = @"- const PREFABS_ROOT : string 'ゲームオブジェクト名'";

            Translator translator = new Translator();
            string output = translator.Translate(input);

            Assert.AreEqual(expected: expected, actual: output);
        }

        [TestMethod]
        public void TestMethod()
        {
            string input = @"        /// <summary>
        /// スコアのスクリプトのキャッシュ
        /// </summary>
        /// <returns></returns>
        public static Score GetScoreScript()
";
            string expected = @"{static} + GetScoreScript() : Score 'スコアのスクリプトのキャッシュ'";

            Translator translator = new Translator();
            string output = translator.Translate(input);

            Assert.AreEqual(expected: expected, actual: output);
        }
    }
}
