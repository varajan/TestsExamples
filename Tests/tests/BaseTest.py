import unittest
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager

from Constants import Constants
from utils import API


class BaseTestCase(unittest.TestCase):
    def setUp(self):
        API.Users.delete_all()
        API.Users.create(Constants.LOGIN)

        options = webdriver.ChromeOptions()
        options.add_argument('ignore-certificate-errors')
        options.add_argument('--headless')
        options.add_argument('--no-sandbox')
        options.add_argument('--disable-dev-shm-usage')
        options.add_argument("--log-level=3")

        self.driver = webdriver.Chrome(ChromeDriverManager().install(), chrome_options=options)

    def tearDown(self) -> None:
        self.driver.quit()
