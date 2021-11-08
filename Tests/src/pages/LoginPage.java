package pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

import utilities.Constants;

public class LoginPage {
	@FindBy(id = "login")
	public WebElement loginFld;
	
	@FindBy(id = "password")
	public WebElement passwordFld;
	
	@FindBy(id = "loginBtn")
	public WebElement loginBtn;
	
	@FindBy(id = "errorMessage")
	public WebElement errorMessage;
	
	private WebDriver driver;
	
	public LoginPage(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(this.driver, this);
	}
	
	public DepositPage login() {
		loginFld.sendKeys(Constants.Login);
		passwordFld.sendKeys(Constants.Password);
		loginBtn.click();

		return new DepositPage(driver);
	}
}
