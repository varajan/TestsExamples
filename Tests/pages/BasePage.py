from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.support.select import Select
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC


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
    def find_elements(self, *locator): return self.driver.find_elements(*locator)

    def get_select(self, select_id): return Select(self.find_element(By.ID, select_id))
    def get_selected_option(self, select_id): return self.get_select(select_id).first_selected_option.text
    def get_select_options(self, select_id): return [option.text for option in self.get_select(select_id).options]
    def select_in_dropdown(self, select_id, value): self.get_select(select_id).select_by_visible_text(value)

    def get_input_value(self, input_id): return self.find_element(By.ID, input_id).get_attribute("value")

    def set_input_value(self, value, input_id):
        control = self.find_element(By.ID, input_id)
        control.click()
        control.clear()
        control.send_keys(value)

    def wait(self, seconds): return WebDriverWait(self.driver, seconds)
    def wait_for_clickable(self, seconds, mark):  self.wait(seconds).until(EC.element_to_be_clickable(mark))

    def wait_for_alert(self, seconds):
        try:
            self.wait(seconds).until(EC.alert_is_present())
            return self.driver.switch_to.alert
        except Exception:
            return None

    def switch_to_frame(self, frame_id):
        self.driver.switch_to.frame(self.find_element(By.ID, frame_id))

    def switch_to_default_frame(self):
        self.driver.switch_to.default_content()
