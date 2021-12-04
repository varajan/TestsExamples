import calendar
import unittest
from datetime import datetime, timedelta

from pages.LoginPage import LoginPage
from tests.BaseTest import BaseTestCase


class DepositPageTests(BaseTestCase):
    def setUp(self):
        super().setUp()
        self.deposit_page = LoginPage(self.driver).login()

    def test_default_fin_year_value_is_365(self):
        self.assertEqual(self.deposit_page.get_fin_year(), "365")

    def test_default_start_date_value_is_today(self):
        self.assertEqual(self.deposit_page.get_start_date(), datetime.now().date())

    def test_days_in_2020_01(self): self.days_in_month("2020", "January", 31)
    def test_days_in_2020_02(self): self.days_in_month("2020", "February", 29)
    def test_days_in_2021_02(self): self.days_in_month("2021", "February", 28)
    def test_days_in_2020_03(self): self.days_in_month("2020", "March", 31)
    def test_days_in_2020_04(self): self.days_in_month("2020", "April", 30)
    def test_days_in_2020_05(self): self.days_in_month("2020", "May", 31)
    def test_days_in_2020_06(self): self.days_in_month("2020", "June", 30)
    def test_days_in_2020_07(self): self.days_in_month("2020", "July", 31)
    def test_days_in_2020_08(self): self.days_in_month("2020", "August", 31)
    def test_days_in_2020_09(self): self.days_in_month("2020", "September", 30)
    def test_days_in_2020_10(self): self.days_in_month("2020", "October", 31)
    def test_days_in_2020_11(self): self.days_in_month("2020", "November", 30)
    def test_days_in_2020_12(self): self.days_in_month("2020", "December", 31)

    def days_in_month(self, year, month, days):
        self.deposit_page.set_start_date_year(year)
        self.deposit_page.set_start_date_month(month)

        expected_values = [str(i) for i in range(1, days+1)]
        actual_values = self.deposit_page.get_start_date_days()

        self.assertEqual(actual_values, expected_values)

    def test_start_date_months(self):
        expected_values = [calendar.month_name[i] for i in range(1, 13)]
        actual_values = self.deposit_page.get_start_date_months()

        self.assertEqual(actual_values, expected_values)

    def test_start_date_years(self):
        expected_values = [str(i) for i in range(2010, 2030)]
        actual_values = self.deposit_page.get_start_date_years()

        self.assertEqual(actual_values, expected_values)

    def test_calculate_end_date(self):
        term = 100
        start_date = datetime.now().date() + timedelta(days=10)
        end_date = (start_date + timedelta(days=term)).strftime("%d/%m/%Y")

        self.deposit_page.set_start_date(start_date)
        self.deposit_page.calculate("1000", "10", str(term))

        self.assertEqual(end_date, self.deposit_page.get_end_date())

    def test_calculation_1(self): self.verify_calculation("6000", "10", "120", "360", "200.00", "6,200.00")
    def test_calculation_2(self): self.verify_calculation("6000", "10", "120", "365", "197.26", "6,197.26")
    def test_calculation_3(self): self.verify_calculation("100000", "99.9", "365", "365", "99,900.00", "199,900.00")
    def test_calculation_4(self): self.verify_calculation("100000", "100.0", "360", "360", "100,000.00", "200,000.00")

    def verify_calculation(self, amount, percent, term, fin_year, interest, income):
        self.deposit_page.calculate(amount, percent, term, fin_year)

        self.assertEqual(interest, self.deposit_page.get_interest())
        self.assertEqual(income, self.deposit_page.get_income())

    def test_max_amount_value_1(self): self.verify_allowed_amount_values("100000", "100000")
    def test_max_amount_value_2(self): self.verify_allowed_amount_values("100001", "0")

    def verify_allowed_amount_values(self, entered, shown):
        self.deposit_page.set_amount(entered)
        self.assertEqual(shown, self.deposit_page.get_amount())

    def test_max_percent_value_1(self): self.verify_allowed_percent_values("100", "100")
    def test_max_percent_value_2(self): self.verify_allowed_percent_values("100.1", "0")

    def verify_allowed_percent_values(self, entered, shown):
        self.deposit_page.set_percent(entered)
        self.assertEqual(shown, self.deposit_page.get_percent())

    def test_max_term_value_1(self): self.verify_allowed_term_values("360", "360", "360")
    def test_max_term_value_2(self): self.verify_allowed_term_values("360", "361", "0")
    def test_max_term_value_3(self): self.verify_allowed_term_values("365", "365", "365")
    def test_max_term_value_4(self): self.verify_allowed_term_values("365", "366", "0")
    def test_max_term_value_5(self): self.verify_allowed_term_values("365", "3.6", "0")

    def verify_allowed_term_values(self, fin_year, entered, shown):
        self.deposit_page.set_fin_year(fin_year)
        self.deposit_page.set_term(entered)
        self.assertEqual(shown, self.deposit_page.get_term())

    if __name__ == '__main__':
        unittest.main()
