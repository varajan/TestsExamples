import unittest
from selenium import webdriver
from selenium.webdriver.chrome.service import Service

from Constants import Constants
from utils import API


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        API.Users.delete_all()
        API.Users.create(Constants.LOGIN)

        options = webdriver.ChromeOptions()
        options.add_argument('ignore-certificate-errors')

        # service = Service("./../chromedriver.exe")
        # self.driver = webdriver.Chrome(service=service, profile=profile)
        service = Service("./../chromedriver")
        self.driver = webdriver.Chrome(service=service, chrome_options=options)

    def tearDown(self) -> None:
        self.driver.quit()
