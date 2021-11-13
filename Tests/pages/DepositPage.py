from selenium.webdriver.common.by import By

from pages.BasePage import BasePage


class DepositPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)

    def get_fin_year(self):
        return "365" if self.find_element(By.XPATH, "//td[text() = '365 days']/input").is_selected() else "360";