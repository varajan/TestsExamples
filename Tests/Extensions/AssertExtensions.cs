using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void ShouldBeTrue(this bool actual, string because = null) => Assert.IsTrue(actual, because);
        public static void ShouldBeFalse(this bool actual, string because = null) => Assert.IsFalse(actual, because);

        public static void ShouldEqual(this string actual, string expected, string because = null) => Assert.AreEqual(expected, actual, because);

        public static void ShouldEqual(this DateTime actual, DateTime expected, string because = null)
        {
            var actualDate = actual.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var expectedDate = expected.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            actualDate.ShouldEqual(expectedDate, because);
        }

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
    }
}
