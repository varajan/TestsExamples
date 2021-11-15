import unittest
from datetime import datetime

from Constants import Constants
from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase
from utils import API
from utils.Formatter import Formatter


class SettingsPageTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        API.Users.create("Settings")
        self.settings_page = LoginPage(self.driver).login("Settings").open_settings()

    def test_logout(self):
        self.settings_page.logout()
        self.assertEqual("Login", self.driver.title)

    def test_date_format_options(self):
        formats = ["dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy"]
        self.assertEqual(formats, self.settings_page.get_date_format_options())

    def test_number_format_options(self):
        formats = ["123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00"]
        self.assertEqual(formats, self.settings_page.get_number_format_options())

    def test_currency_options(self):
        currencies = ["$ - US dollar", "€ - euro", "£ - Great Britain Pound"]
        self.assertEqual(currencies, self.settings_page.get_currency_options())

    def test_change_number_format_1(self): self.change_number_format("123.456.789,00", "127.397,26", "27.397,26")
    def test_change_number_format_4(self): self.change_number_format("123,456,789.00", "127,397.26", "27,397.26")
    def test_change_number_format_2(self): self.change_number_format("123 456 789.00", "127 397.26", "27 397.26")
    def test_change_number_format_3(self): self.change_number_format("123 456 789,00", "127 397,26", "27 397,26")

    def change_number_format(self, number_format, income, interest):
        deposit_page = self.settings_page.set_number_format(number_format).save()
        deposit_page.calculate("100000", "100", "100")

        self.assertEqual(interest, deposit_page.get_interest())
        self.assertEqual(income, deposit_page.get_income())

    def test_change_date_format_1(self): self.change_date_format("dd/MM/yyyy")
    def test_change_date_format_2(self): self.change_date_format("dd-MM-yyyy")
    def test_change_date_format_3(self): self.change_date_format("MM/dd/yyyy")
    def test_change_date_format_4(self): self.change_date_format("MM dd yyyy")

    def change_date_format(self, date_format):
        end_date = Formatter.format_date(datetime.now().date(), date_format)
        deposit_page = self.settings_page.set_date_format(date_format).save()

        self.assertEqual(end_date, deposit_page.get_end_date())

    def test_change_currency_1(self): self.change_currency("$", "$ - US dollar")
    def test_change_currency_2(self): self.change_currency("€", "€ - euro")
    def test_change_currency_3(self): self.change_currency("£", "£ - Great Britain Pound")

    def change_currency(self, symbol, currency):
        deposit_page = self.settings_page.set_currency(currency).save()

        self.assertEqual(symbol, deposit_page.get_currency())

    def test_cancel_changes(self):
        self.settings_page.set_number_format("123 456 789.00")
        self.settings_page.set_date_format("MM dd yyyy")
        self.settings_page.set_currency("€ - euro")
        self.settings_page = self.settings_page.cancel().open_settings()

        self.assertEqual(Constants.NUMBER_FORMAT, self.settings_page.get_number_format())
        self.assertEqual(Constants.DATE_FORMAT, self.settings_page.get_date_format())
        self.assertEqual(Constants.CURRENCY, self.settings_page.get_currency())

    if __name__ == '__main__':
        unittest.main()
