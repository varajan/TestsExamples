from time import sleep
from selenium.webdriver.common.by import By

from pages.BasePage import BasePage


class HistoryPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)
        sleep(1)

    def is_history_empty(self): return self.get_history_size() == 0
    def get_history_size(self): return len(self.find_elements(By.XPATH, "//table//tr[td]"))
    def get_history(self): return [self.get_history_row(row) for row in range(1, self.get_history_size()+1)]

    def get_history_row(self, index):
        row = self.find_element(By.XPATH, "//table//tr[td][" + str(index) + "]")
        cells = row.find_elements(By.XPATH, ".//td")
        return [cell.text for cell in cells]

    def clear(self):
        from pages.DepositPage import DepositPage
        self.find_element(By.ID, "clear").click()
        return DepositPage(self.driver)
