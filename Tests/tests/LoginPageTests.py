import unittest

from Constants import Constants
from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase


class LoginPageTests(BaseTestCase):
    def test_empty_login_and_password(self): self.negative("", "")
    def test_empty_login(self):              self.negative("", Constants.Password)
    def test_empty_password(self):           self.negative(Constants.Login, "")
    def test_valid_only_login(self):         self.negative(Constants.Login, Constants.Password + "!")
    def test_valid_only_password(self):      self.negative(Constants.Login + "!", Constants.Password)
    def test_valid_login_and_password(self): self.positive(Constants.Login, Constants.Password)

    def negative(self, login, password):
        login_page = LoginPage(self.driver)
        error = login_page.login(login, password)

        self.assertEqual(error, "Incorrect user name or password!")

    def positive(self, login, password):
        login_page = LoginPage(self.driver)
        login_page.login(login, password)

        self.assertEqual(self.driver.title, "Deposit calculator")


if __name__ == '__main__':
    unittest.main()
