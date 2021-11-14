from time import sleep

from selenium.webdriver.common.by import By

from pages.BasePage import BasePage


class SettingsPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)
        sleep(1)

    def get_date_format_options(self): return self.get_select_options("dateFormat")
    def get_number_format_options(self): return self.get_select_options("numberFormat")
    def get_currency_options(self): return self.get_select_options("currency")

    def get_number_format(self): return self.get_selected_option("numberFormat")

    def set_number_format(self, value):
        self.select_in_dropdown("numberFormat", value)
        return self

    def get_date_format(self): return self.get_selected_option("dateFormat")

    def set_date_format(self, value):
        self.select_in_dropdown("dateFormat", value)
        return self

    def get_currency(self): return self.get_selected_option("currency")

    def set_currency(self, value):
        self.select_in_dropdown("currency", value)
        return self

    def save(self):
        from pages.DepositPage import DepositPage
        self.find_element(By.ID, "save").click()
        return DepositPage(self.driver)

    def cancel(self):
        from pages.DepositPage import DepositPage
        self.find_element(By.ID, "cancel").click()
        return DepositPage(self.driver)

    def logout(self):
        self.find_element(By.XPATH, "//div[text() = 'Logout']").click()
