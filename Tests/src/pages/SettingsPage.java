package pages;

import java.util.List;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class SettingsPage extends BasePage {
	@FindBy(id = "dateFormat")
	private WebElement dateFormat;

	@FindBy(id = "numberFormat")
	private WebElement numberFormat;

	@FindBy(id = "currency")
	private WebElement currency;

	@FindBy(id = "save")
	private WebElement save;

	@FindBy(id = "cancel")
	private WebElement cancel;

	@FindBy(xpath = "//div[text() = 'Logout']")
	private WebElement logout;
	
	public SettingsPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);
	}

	public String getDateFormat() { return getSelectValue(dateFormat); }
	public void setDateFormat(String value) { setSelectValue(dateFormat, value); }
	public List<String> getDateFormats(){ return getSelectOptions(dateFormat); }

	public String getNumberFormat() { return getSelectValue(numberFormat); }
	public void setNumberFormat(String value) { setSelectValue(numberFormat, value); }
	public List<String> getNumberFormats(){ return getSelectOptions(numberFormat); }

	public String getCurrency() { return getSelectValue(currency); }
	public void setCurrency(String value) { setSelectValue(currency, value); }
	public List<String> getCurrencies(){ return getSelectOptions(currency); }

	public SettingsPage set(String dateFormat, String numberFormat, String currency) {
		setDateFormat(dateFormat);
		setNumberFormat(numberFormat);
		setCurrency(currency);
		
		return this;
	}
	
	public DepositPage save() {
		save.click();
		return new DepositPage(driver);
	}
	
	public DepositPage cancel() {
		cancel.click();
		return new DepositPage(driver);
	}
	
	public LoginPage logout() {
		logout.click();
		return new LoginPage(driver);
	}
}