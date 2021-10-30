using NUnit.Framework;

namespace Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void ShouldBeTrue(this bool actual, string because = null) => Assert.IsTrue(actual, because);
        public static void ShouldBeFalse(this bool actual, string because = null) => Assert.IsFalse(actual, because);

        public static void ShouldEqual(this string actual, string expected, string because = null) => Assert.AreEqual(expected, actual, because);
    }
}
