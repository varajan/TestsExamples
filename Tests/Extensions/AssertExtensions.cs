using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void ShouldBeFalse(this bool actual, string because = null) => Assert.IsFalse(actual, because);

        public static void ShouldEqual(this string actual, string expected, string because = null) => Assert.AreEqual(expected, actual, because);

        public static void ShouldEqual(this IEnumerable<string> actual, IEnumerable<string> expected, string because = null)
        {
            var error = string.Empty;
            var actualList = actual.ToList();
            var expectedList = expected.ToList();

            if (actualList.Count != expectedList.Count)
            {
                error += $"{Environment.NewLine}Lists have different count. Expected: {expectedList.Count}, actual: {actualList.Count}";
            }

            for (var i = 0; i < Math.Min(actualList.Count, expectedList.Count); i++)
            {
                if (expectedList[i] != actualList[i])
                {
                    error += $"{Environment.NewLine}[{i}] Expected: {expectedList[i]}, actual: {actualList[i]}";
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                Assert.Fail(because + error);
            }
        }

        public static void ShouldEqual(this IEnumerable<IEnumerable<string>> actual, IEnumerable<IEnumerable<string>> expected, string because = null)
        {
            var error = string.Empty;
            var actualList = actual.Select(x => x.ToList()).ToList();
            var expectedList = expected.Select(x => x.ToList()).ToList();

            if (actualList.Count != expectedList.Count)
            {
                error += $"{Environment.NewLine}Lists have different rows count. Expected: {expectedList.Count}, actual: {actualList.Count}";
            }

            for (var i = 0; i < Math.Min(actualList.Count, expectedList.Count); i++)
            {
                if (actualList[i].Count != expectedList[i].Count)
                {
                    error += $"{Environment.NewLine}[{i}] Row have different count. Expected: {actualList[i].Count}, actual: {actualList[i].Count}";
                }

                for (var j = 0; j < Math.Min(actualList[i].Count, expectedList[i].Count); j++)
                {
                    if (expectedList[i][j] != actualList[i][j])
                    {
                        error += $"{Environment.NewLine}[{i} - {j}] Expected: {expectedList[i][j]}, actual: {actualList[i][j]}";
                    }
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                Assert.Fail(because + error);
            }
        }
    }
}
