using NUnit.Framework;
using System.Collections.Generic;
using TimManager.Models;

namespace TimManager.Ui.Tests
{
    public class UtilsTests
    {
        [TestCase("qwer", ExpectedResult = true)]
        [TestCase("qwer1", ExpectedResult = true)]
        [TestCase("qwer2", ExpectedResult = true)]
        [TestCase("qwer3", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("                              ", ExpectedResult = false)]
        public bool HasString(string someText)
        {
            return someText.Has();
        }

        [Test, TestCaseSource(nameof(collectionAndBoolLikeExpectedCases))]
        public void HasCollection<T>(ICollection<T> collection, bool expected)
        {
            bool actual = collection.Has();

            Assert.AreEqual(expected, actual);
        }

        private static object[] collectionAndBoolLikeExpectedCases =
        {
            new object[] { new List<int>() { 0, 1, 1 }, true  },
            new object[] { new List<float>() { 0f, 1f, 1f }, true },
            new object[] { new List<string>() { "0", "2" }, true },
            new object[] { new List<int>(), false },
            new object[] { new List<string>(), false }
        };
    }
}