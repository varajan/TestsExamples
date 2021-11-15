import unittest
from selenium import webdriver
from selenium.webdriver.chrome.service import Service

from Constants import Constants
from utils import API


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        API.Users.delete_all()
        API.Users.create(Constants.LOGIN)

        self.service = Service("./../chromedriver.exe")
        self.driver = webdriver.Chrome(service=self.service)

    def tearDown(self) -> None:
        self.driver.quit()
