import unittest

from Constants import Constants
from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase
from utils import API


class HistoryPageTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        API.Users.create("History")
        self.deposit_page = LoginPage(self.driver).login("History")

    def test_clear_history(self):
        self.deposit_page.calculate("1000", "99", "25")
        self.deposit_page.calculate("1000", "55", "10")

        history_page = self.deposit_page.open_history()
        history_page.clear()

        self.assertTrue(history_page.is_history_empty())

    def test_history_values(self):
        row_1 = self.deposit_page.calculate("1000", "99", "25").get_data(Constants.DATE_FORMAT, Constants.NUMBER_FORMAT)
        row_2 = self.deposit_page.calculate("5000", "55", "10").get_data(Constants.DATE_FORMAT, Constants.NUMBER_FORMAT)
        history_page = self.deposit_page.open_history()

        self.assertEqual(2, history_page.get_history_size())
        self.assertEqual(row_2, history_page.get_history_row(1))
        self.assertEqual(row_1, history_page.get_history_row(2))

    def test_history_shows_last_9_rows(self):
        rows = [
            self.deposit_page.calculate("10", "5", str(term)).get_data(Constants.DATE_FORMAT, Constants.NUMBER_FORMAT)
            for term in range(1, 12)
        ][::-1][:9]

        history_page = self.deposit_page.open_history()

        self.assertEqual(9, history_page.get_history_size())
        self.assertEqual(rows, history_page.get_history())

    def test_history_with_settings_1(self): self.history_with_settings("123.456.789,00", "dd-MM-yyyy")
    def test_history_with_settings_2(self): self.history_with_settings("123 456 789.00", "MM/dd/yyyy")
    def test_history_with_settings_3(self): self.history_with_settings("123 456 789,00", "MM dd yyyy")

    def history_with_settings(self, number_format, date_format):
        settings_page = self.deposit_page.open_settings()
        settings_page.set_number_format(number_format)
        settings_page.set_date_format(date_format)
        self.deposit_page = settings_page.save()

        history = self.deposit_page.calculate("100000", "99", "199").get_data(date_format, number_format)
        history_page = self.deposit_page.open_history()

        self.assertEqual(history, history_page.get_history_row(1))


if __name__ == '__main__':
    unittest.main()
