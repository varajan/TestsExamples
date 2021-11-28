using System;
using System.Globalization;
using Atata;
using NUnit.Framework;
using Tests.Pages;

namespace Tests.Tests
{
    [TestFixture]
    public class SettingsPageTests : BaseTest
    {
        public SettingsPage OpenSettings() => OpenDepositPage().OpenSettings();

        [Test]
        public void DateFormatOptionsTest() =>
            OpenSettings()
                .DateFormat.Options
                    .Should.BeEquivalent("dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy");

        [Test]
        public void NumberFormatOptionsTest() =>
            OpenSettings()
                .NumberFormat.Options
                    .Should.BeEquivalent("123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00");

        [Test]
        public void CurrencyOptionsTest() =>
            OpenSettings()
                .Currency.Options
                    .Should.BeEquivalent("$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia");

        [TestCase("123,456,789.00", "127,397.26", "27,397.26")]
        [TestCase("123.456.789,00", "127.397,26", "27.397,26")]
        [TestCase("123 456 789.00", "127 397.26", "27 397.26")]
        [TestCase("123 456 789,00", "127 397,26", "27 397,26")]
        public void ChangeNumberFormatTest(string format, string income, string interest) =>
            OpenSettings()
                .NumberFormat.Set(format).Save()
                .Populate("100000", "100", "100").Calculate()
                .Income.Should.Equal(income)
                .InterestEarned.Should.Equal(interest);


        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        [TestCase("MM dd yyyy")]
        public void ChangeDateFormatTest(string format) =>
            OpenSettings()
                .DateFormat.Set(format).Save()
                .EndDate.Should
                    .Equal(DateTime.Today.ToString(format, CultureInfo.InvariantCulture));

        [TestCase("$", "$ - US dollar")]
        [TestCase("€", "€ - euro")]
        [TestCase("£", "£ - Great Britain Pound")]
        [TestCase("₴", "₴ - Ukrainian hryvnia")]
        public void ChangeCurrencyTest(string symbol, string currency) =>
            OpenSettings()
                .Currency.Set(currency).Save()
                .Currency.Should.Equal(symbol);

        [Test]
        public void LogoutTest() =>
            OpenSettings()
                .Logout()
                .PageTitle.Should.Equal("Login");

        [Test]
        public void CancelSettingsChangesTest() =>
            OpenSettings()
                .DateFormat.Set("MM/dd/yyyy")
                .NumberFormat.Set("123 456 789,00")
                .Currency.Set("€ - euro")
                .Cancel()
                .OpenSettings()
                .DateFormat.Should.Equal(Defaults.DateFormat)
                .NumberFormat.Should.Equal(Defaults.NumberFormat)
                .Currency.Should.Equal(Defaults.Currency);
    }
}
