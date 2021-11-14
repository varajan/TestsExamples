import calendar
from datetime import datetime
from time import sleep

from selenium.webdriver.common.by import By

from pages.BasePage import BasePage


class DepositPage(BasePage):
    def __init__(self, driver):
        super().__init__(driver)
        sleep(1)

    def get_amount(self): return self.get_input_value("amount")
    def set_amount(self, amount): self.set_input_value(amount, "amount")

    def get_percent(self): return self.get_input_value("percent")
    def set_percent(self, percent): self.set_input_value(percent, "percent")

    def get_term(self): return self.get_input_value("term")
    def set_term(self, term): self.set_input_value(term, "term")

    def get_fin_year(self):
        return "365" if self.find_element(By.XPATH, "//td[text() = '365 days']/input").is_selected() else "360"

    def set_fin_year(self, fin_year):
        if not(fin_year in ["360", "365"]):
            raise ValueError("Only 360 and 365 values are allowed")

        self.find_element(By.XPATH, "//td[text() = '" + fin_year + " days']/input").click()

    def get_start_date_days(self): return self.get_select_options("day")
    def get_start_date_months(self): return self.get_select_options("month")
    def get_start_date_years(self): return self.get_select_options("year")

    def set_start_date_day(self, value): self.select_in_dropdown("day", value)
    def set_start_date_month(self, value): self.select_in_dropdown("month", value)
    def set_start_date_year(self, value): self.select_in_dropdown("year", value)

    def set_start_date(self, date):
        self.get_select("day").select_by_visible_text(str(date.year))
        self.get_select("month").select_by_visible_text(calendar.month_name[date.month])
        self.get_select("year").select_by_visible_text(str(date.day))

    def get_start_date(self):
        d = self.get_select("day").first_selected_option.text
        m = list(calendar.month_name).index(self.get_select("month").first_selected_option.text)
        y = self.get_select("year").first_selected_option.text

        return datetime(int(y), m, int(d)).date()

    def get_end_date(self): return self.get_input_value("endDate")
    def get_interest(self): return self.get_input_value("interest")
    def get_income(self): return self.get_input_value("income")

    def calculate(self, amount, percent, term, fin_year="365"):
        self.set_amount(amount)
        self.set_percent(percent)
        self.set_term(term)
        self.set_fin_year(fin_year)
        self.find_element(By.ID, "calculateBtn").click()
        # wait for enabled
