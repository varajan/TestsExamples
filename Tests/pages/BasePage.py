from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.common.by import By
from selenium.webdriver.support.select import Select


class BasePage(object):
    def __init__(self, driver):
        self.driver = driver

    def element_exists(self, *locator):
        try:
            self.driver.find_element(*locator)
        except NoSuchElementException:
            return False
        return True

    def find_element(self, *locator): return self.driver.find_element(*locator)

    def get_select(self, select_id): return Select(self.find_element(By.ID, select_id))
    def get_select_options(self, select_id): return [option.text for option in self.get_select(select_id).options]
    def select_in_dropdown(self, select_id, value): self.get_select(select_id).select_by_visible_text(value)

    def get_input_value(self, input_id): return self.find_element(By.ID, input_id).get_attribute("value")

    def set_input_value(self, value, input_id):
        control = self.find_element(By.ID, input_id)
        control.click()
        control.clear()
        control.send_keys(value)
