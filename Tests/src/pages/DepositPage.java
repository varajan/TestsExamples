package pages;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.List;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.Select;

import utilities.Dates;

public class DepositPage {
	@FindBy(id = "amount")
	private WebElement amount;
	
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
	
	private WebDriver driver;
	
	public DepositPage(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(this.driver, this);
	}

	public Boolean isOpened() {
		return driver.getTitle().equals("Deposit calculator");
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
		Select daySelect = new Select(day);
		Select monthSelect = new Select(month);
		Select yearSelect = new Select(year);
		
		String dayValue = String.format("%02d", Integer.parseInt(daySelect.getFirstSelectedOption().getText()));
		String monthValue = String.format("%02d", Dates.getMonthNumberFromName(monthSelect.getFirstSelectedOption().getText()));
		String yearValue = yearSelect.getFirstSelectedOption().getText();
		
		return dayValue + "/" + monthValue + "/" + yearValue;
	}
	
	public void setStartDate(String value) throws ParseException {
		new Select(year).selectByVisibleText(Dates.formatDate(value, "yyyy"));
		new Select(month).selectByVisibleText(Dates.formatDate(value, "MMMM"));
		new Select(day).selectByVisibleText(Dates.formatDate(value, "d"));
	}
	
	public List<String> getDays(){
		return getOptions(new Select(day));
	}

	public void setMonth(String value) {
		new Select(month).selectByVisibleText(value);
	}
	
	public List<String> getMonths(){
		return getOptions(new Select(month));
	}
	
	public void setYear(String value) {
		new Select(year).selectByVisibleText(value);
	}
	
	public List<String> getYears(){
		return getOptions(new Select(year));
	}
	
	private List<String> getOptions(Select select){
		List<String> result = new ArrayList<String>();
		
		for (WebElement option : select.getOptions()) {
			result.add(option.getText());
		}
		
		return result;
	}
	
	public String getAmount() {
		return amount.getAttribute("value");
	}
	public void setAmount(String value) {
		amount.sendKeys(value);
	}
	
	public String getPercent() {
		return percent.getAttribute("value");
	}
	public void setPercent(String value) {
		percent.sendKeys(value);
	}
	
	public String getTerm() {
		return term.getAttribute("value");
	}
	public void setTerm(String value) {
		term.sendKeys(value);
	}
	
	public void populate(String amount, String percent, String term) {
		setAmount(amount);
		setPercent(percent);
		setTerm(term);
	}
	
	public void calculate() {
		calculateBtn.click();
	}

	public String getEndDate() {
		return endDate.getAttribute("value");
	}

	public String getInterest() {
		return interest.getAttribute("value");
	}

	public String getIncome() {
		return income.getAttribute("value");
	}
}
