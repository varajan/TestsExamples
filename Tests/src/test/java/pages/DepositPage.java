package test.java.pages;

import java.text.ParseException;
import java.util.List;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

import com.google.common.collect.Lists;
import test.java.data.Constants;
import test.java.utilities.Dates;
import test.java.utilities.Strings;

public class DepositPage extends BasePage {
	@FindBy(id = "amount")
	private WebElement amount;
	
	@FindBy(id = "currency")
	private WebElement currency;
	
	@FindBy(id = "percent")
	private WebElement percent;
	
	@FindBy(id = "term")
	private WebElement term;
	
	@FindBy(xpath = "//td[text() = '360 days']/input")
	private WebElement finYear360;
	
	@FindBy(xpath = "//td[text() = '365 days']/input")
	private WebElement finYear365;
	
	@FindBy(id = "day")
	private WebElement day;
	
	@FindBy(id = "month")
	private WebElement month;
	
	@FindBy(id = "year")
	private WebElement year;
	
	@FindBy(id = "calculateBtn")
	private WebElement calculateBtn;
	
	@FindBy(id = "endDate")
	private WebElement endDate;
	
	@FindBy(id = "interest")
	private WebElement interest;
	
	@FindBy(id = "income")
	private WebElement income;
	
	@FindBy(xpath = "//div[text() = 'History']")
	private WebElement history;
	
	@FindBy(xpath = "//div[text() = 'Settings']")
	private WebElement settings;
	
	public DepositPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);
		
		waitForElementVisible(amount);
	}

	public String getCurrency() {
		return currency.getText();
	}
	
	public String getFinYear() {
		if (finYear360.isSelected()) return "360";
		if (finYear365.isSelected()) return "365";
		
		return "";
	}

	public void setFinYear(String value) {
		switch (value) {
		case "360":
			finYear360.click();
			break;

		case "365":
			finYear365.click();
			break;
		}
	}
	
	public String getStartDate() throws ParseException {
		return getDay() + "/" + getMonth() + "/" + getYear();
	}
	
	public void setStartDate(String value) throws ParseException {
		String yyyy = Dates.formatDate(value, "yyyy");
		String mmmm = Dates.formatDate(value, "MMMM");
		String d = Dates.formatDate(value, "d");
		
		setYear(yyyy);
		setMonth(mmmm);
		setDay(d);
	}
	
	public String getDay() {
		String d = getSelectValue(day);
		String dd = String.format("%02d", Integer.parseInt(d));
		return dd;
	}
	public void setDay(String value) { setSelectValue(day, value); }
	public List<String> getDays(){ return getSelectOptions(day); }

	public String getMonth() throws ParseException {
		String mmmm = getSelectValue(month);
		int mm = Dates.getMonthNumberFromName(mmmm);
		return String.format("%02d", mm);
	}
	public void setMonth(String value) { setSelectValue(month, value); }
	public List<String> getMonths(){ return getSelectOptions(month); }
	
	public String getYear() { return getSelectValue(year); }
	public void setYear(String value) { setSelectValue(year, value); }
	public List<String> getYears(){ return getSelectOptions(year); }

	public String getAmount() { return getInputValue(amount); }
	public void setAmount(String value) { setInputValue(amount, value); }
	
	public String getPercent() { return getInputValue(percent); }
	public void setPercent(String value) { setInputValue(percent, value); }

	public String getTerm() { return getInputValue(term); }
	public void setTerm(String value) { setInputValue(term, value); }

	public DepositPage populate(String amount, String percent, String term) {
		setAmount(amount);
		setPercent(percent);
		setTerm(term);
		
		return this;
	}
	
	public DepositPage calculate() {
		calculateBtn.click();
		waitForElementToBeClickable(calculateBtn);

		return this;
	}

	public List<String> getData() throws ParseException{
		return getData(Constants.DefaultDateFormat, Constants.DefaultNumberFormat);
	}

	public List<String> getData(String dateFormat, String numberFormat) throws ParseException{
		return Lists.newArrayList(
			Strings.formatNumber(getAmount(), numberFormat),
			getPercent() + "%",
			getTerm(),
			getFinYear(),
			Dates.formatDate(getStartDate(), dateFormat),
			getEndDate(),
			getInterest(),
			getIncome()
		);
	}
	
	public String getEndDate()  { return getInputValue(endDate); }
	public String getInterest() { return getInputValue(interest); }
	public String getIncome()   { return getInputValue(income); }
	
	public SettingsPage openSettings() {
		settings.click();
		
		return new SettingsPage(driver);
	}
	
	public HistoryPage openHistory() {
		history.click();
		
		return new HistoryPage(driver);
	}
}
