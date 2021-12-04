import unittest

from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase


class RegisterPageTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        self.register_page = LoginPage(self.driver).open_registration()

    def test_register_and_login(self):
        result = self.register_page.register("UserName", "password1", "password1", "UserName@password.email")

        self.assertTrue(result.is_successful)
        self.assertEqual(result.message, "Registration was successful.")
        self.assertEqual(self.driver.title, "Login")

        result.page.login("username", "password1")
        self.assertEqual(self.driver.title, "Deposit calculator")

    def test_login_is_registered(self):
        self.negative_test("Test", "password", "password", "some-email@test.com",
                           "User with this login is already registered.")

    def test_email_is_registered(self):
        self.negative_test("User", "password", "password", "test@test.com",
                           "User with this email is already registered.")

    def test_password_mismatch(self):
        self.negative_test("User", "password", "passwort", "some-email@test.com", "Passwords are different.")

    def test_invalid_email_1(self):
        self.negative_test("User", "password", "password", "@test.com", "Invalid email.")

    def test_invalid_email_2(self):
        self.negative_test("User", "password", "password", "some.test.com", "Invalid email.")

    def test_invalid_password(self):
        self.negative_test("User", "pass", "pass", "some@test.com", "Password is too short.")

    def negative_test(self, login, password1, password2, email, error):
        result = self.register_page.register(login, password1, password2, email)

        self.assertFalse(result.is_successful)
        self.assertEqual(error, result.message)


if __name__ == '__main__':
    unittest.main()
