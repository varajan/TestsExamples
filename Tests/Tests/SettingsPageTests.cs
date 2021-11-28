using System;
using System.Globalization;
using NUnit.Framework;
using Tests.Data;
using Tests.Extensions;

namespace Tests.Tests
{
    public class SettingsPageTests : BaseTest
    {
        [SetUp]
        public void OpenSettingsPage()
        {
            LoginPage.Login(Defaults.Login, Defaults.Password);
            DepositPage.OpenSettings();
        }

        [Test]
        public void DateFormatOptionsTest()
        {
            var formats = new[] { "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy" };

            SettingsPage.DateFormats.ShouldEqual(formats);
        }

        [Test]
        public void NumberFormatOptionsTest()
        {
            var formats = new[] { "123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00" };

            SettingsPage.NumberFormats.ShouldEqual(formats);
        }

        [Test]
        public void CurrencyOptionsTest()
        {
            var options = new[] { "$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia" };

            SettingsPage.Currencies.ShouldEqual(options);
        }

        [TestCase("123,456,789.00", "127,397.26", "27,397.26")]
        [TestCase("123.456.789,00", "127.397,26", "27.397,26")]
        [TestCase("123 456 789.00", "127 397.26", "27 397.26")]
        [TestCase("123 456 789,00", "127 397,26", "27 397,26")]
        public void ChangeNumberFormatTest(string format, string income, string interest)
        {
            // Arrange
            SettingsPage.NumberFormat = format;
            SettingsPage.Save();

            // Act
            DepositPage.Populate("100000", "100", "100");
            DepositPage.Calculate();

            // Assert
            DepositPage.Income.ShouldEqual(income);
            DepositPage.InterestEarned.ShouldEqual(interest);
        }

        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        [TestCase("MM dd yyyy")]
        public void ChangeDateFormatTest(string format)
        {
            // Arrange
            var expectedDate = DateTime.Today.ToString(format, CultureInfo.InvariantCulture);

            // Act
            SettingsPage.DateFormat = format;
            SettingsPage.Save();

            // Assert
            DepositPage.EndDate.ShouldEqual(expectedDate);
        }

        [Test]
        public void LogoutTest()
        {
            SettingsPage.Logout();

            LoginPage.IsOpened.ShouldBeTrue($"Expected '{LoginPage.PageName}' page to be opened, but '{LoginPage.CurrentPageName}' was found.");
        }

        [TestCase("$", "$ - US dollar")]
        [TestCase("€", "€ - euro")]
        [TestCase("£", "£ - Great Britain Pound")]
        [TestCase("₴", "₴ - Ukrainian hryvnia")]
        public void ChangeCurrencyTest(string symbol, string currency)
        {
            SettingsPage.Currency = currency;
            SettingsPage.Save();

            DepositPage.Currency.ShouldEqual(symbol);
        }

        [Test]
        public void SaveSettingsChangesTest()
        {
            // Arrange
            SettingsPage.ResetToDefaults();
            SettingsPage.Open();

            // Act
            SettingsPage.Currency = "€ - euro";
            SettingsPage.NumberFormat = "123 456 789,00";
            SettingsPage.DateFormat = "MM/dd/yyyy";
            SettingsPage.Save();
            SettingsPage.Open();

            // Assert
            SettingsPage.Currency.ShouldEqual("€ - euro");
            SettingsPage.NumberFormat.ShouldEqual("123 456 789,00");
            SettingsPage.DateFormat.ShouldEqual("MM/dd/yyyy");
        }

        [Test]
        public void CancelSettingsChangesTest()
        {
            // Arrange
            SettingsPage.ResetToDefaults();
            SettingsPage.Open();

            // Act
            SettingsPage.Currency = "€ - euro";
            SettingsPage.NumberFormat = "123 456 789,00";
            SettingsPage.DateFormat = "MM/dd/yyyy";
            SettingsPage.Cancel();
            SettingsPage.Open();

            // Assert
            SettingsPage.Currency.ShouldEqual(Defaults.Currency);
            SettingsPage.NumberFormat.ShouldEqual(Defaults.NumberFormat);
            SettingsPage.DateFormat.ShouldEqual(Defaults.DateFormat);
        }
    }
}
