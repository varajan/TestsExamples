import unittest
from selenium import webdriver
from selenium.webdriver.chrome.service import Service


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        self.service = Service("./../chromedriver.exe")
        self.driver = webdriver.Chrome(service=self.service)
        self.driver.maximize_window()

    def tearDown(self):
        self.driver.quit()
