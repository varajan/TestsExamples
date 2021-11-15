import unittest

from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase


class RemindPasswordTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        self.login_page = LoginPage(self.driver)

    def test_open_close_remind_password_view(self):
        self.login_page.remind_password().open()
        self.login_page.remind_password().close()

        self.assertFalse(self.login_page.remind_password().is_opened())

    def test_empty_email(self): self.send_reminder_test("", False, "Invalid email.")
    def test_invalid_email_1(self): self.send_reminder_test("invalid @.email", False, "Invalid email.")
    def test_invalid_email_2(self): self.send_reminder_test("@invalid.email", False, "Invalid email.")
    def test_not_existed_user(self): self.send_reminder_test("test@test.email", False, "No user was found.")

    def test_existed_user(self):
        self.send_reminder_test("Test@test.COM", True, "Email with instructions was sent to test@test.com")

    def send_reminder_test(self, email, is_successful, message):
        result = self.login_page.remind_password().open().send(email)

        self.assertEqual(is_successful, result.is_successful)
        self.assertEqual(message, result.message)

    if __name__ == '__main__':
        unittest.main()
