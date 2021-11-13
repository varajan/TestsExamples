from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.common.by import By
from Constants import Constants


class BasePage(object):
    def __init__(self, driver):
        self.driver = driver

    def element_exists(self, *locator):
        try:
            self.driver.find_element(*locator)
        except NoSuchElementException:
            return False
        return True

    def find_element(self, *locator):
        return self.driver.find_element(*locator)
