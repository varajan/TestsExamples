from selenium.webdriver.common.by import By
from Constants import Constants


class BasePage(object):
    def __init__(self, driver):
        self.driver = driver

    def get_title(self): self.driver.title
