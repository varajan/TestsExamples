import unittest

from Constants import Constants
from pages.LoginPage import LoginPage
from pages.DepositPage import DepositPage
from tests.BaseTest import BaseTestCase


class DepositPageTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        self.deposit_page = LoginPage(self.driver).login(Constants.Login, Constants.Password)

    def test_default_fin_year_value_is_365(self):
        self.assertEqual(self.deposit_page.get_fin_year(), "365")

    if __name__ == '__main__':
        unittest.main()
