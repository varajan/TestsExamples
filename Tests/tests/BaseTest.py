import unittest
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager

from Constants import Constants
from utils import API


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        API.Users.delete_all()
        API.Users.create(Constants.LOGIN)

        # service = Service("./../chromedriver")
        options = webdriver.ChromeOptions()
        options.add_argument('ignore-certificate-errors')

        # service = Service("./../chromedriver.exe")
        # self.driver = webdriver.Chrome(service=service, profile=profile)
        # self.driver = webdriver.Chrome(service=service, chrome_options=options)
        self.driver = webdriver.Chrome(ChromeDriverManager().install(), chrome_options=options)

    def tearDown(self) -> None:
        self.driver.quit()
