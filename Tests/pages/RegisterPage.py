from selenium.webdriver.common.by import By

from pages.BasePage import BasePage
from utils.Result import Result


class RegisterPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)

    def register(self, login, password1, password2, email):
        from pages.LoginPage import LoginPage

        self.set_input_value(login, "login")
        self.set_input_value(email, "email")
        self.set_input_value(password1, "password1")
        self.set_input_value(password2, "password2")
        self.find_element(By.ID, "register").click()

        alert = self.wait_for_alert(1)

        if alert is None:
            return Result(False, self.find_element(By.ID, "errorMessage").text, self)

        alert_text = alert.text
        alert.accept()

        return Result(True, alert_text, LoginPage(self.driver))
