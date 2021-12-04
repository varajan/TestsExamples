from selenium.webdriver.common.by import By

from pages.BasePage import BasePage
from utils.Result import Result


class RemindPasswordFrame(BasePage):
    def __init__(self, driver, remind_btn):
        self.button = remind_btn
        super().__init__(driver)

    frame_id = "remindPasswordView"

    def is_opened(self):
        self.switch_to_default_frame()
        return self.find_element(By.ID, self.frame_id).is_displayed()

    def open(self):
        if not self.is_opened():
            self.button.click()
        return self

    def close(self):
        self.switch_to_frame(self.frame_id)
        self.find_element(By.XPATH, "//button[text() = 'x']").click()
        self.switch_to_default_frame()

    def send(self, email):
        self.switch_to_frame(self.frame_id)
        self.set_input_value(email, "email")
        self.find_element(By.XPATH, "//button[text() = 'Send']").click()

        alert = self.wait_for_alert(1)

        if alert is None:
            return Result(False, self.find_element(By.ID, "message").text, self)

        result = Result(True, alert.text, self)
        alert.accept()
        return result
