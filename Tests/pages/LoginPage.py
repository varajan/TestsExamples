from time import sleep

from selenium.webdriver.common.by import By
from Constants import Constants
from pages.BasePage import BasePage
from pages.DepositPage import DepositPage


class LoginPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)
        self.driver.get(Constants.URL + "/Login")
        sleep(1)

    def login(self, login, password):
        self.set_input_value(login, "login")
        self.set_input_value(password, "password")
        self.find_element(By.ID, "loginBtn").click()
        sleep(1)

        if self.element_exists(By.ID, "errorMessage"):
            return self.find_element(By.ID, "errorMessage").text

        return DepositPage(self.driver)
