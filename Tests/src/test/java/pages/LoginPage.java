package test.java.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import test.java.data.Constants;

public class LoginPage extends BasePage {
	@FindBy(id = "login")
	private WebElement loginFld;
	
	@FindBy(id = "password")
	private WebElement passwordFld;
	
	@FindBy(id = "loginBtn")
	private WebElement loginBtn;
	
	@FindBy(id = "errorMessage")
	private WebElement errorMessage;

	@FindBy(xpath = "//div[text() = 'Register']")
	private WebElement register;

	public LoginPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);
	}

	public RemindPasswordView remindPasswordView = new RemindPasswordView(driver);
	
	public RegisterPage openRegistration() {
		register.click();
		
		return new RegisterPage(driver);
	}
	
	public DepositPage login() {
		return login(Constants.Login, Constants.Password);
	}

	public DepositPage login(String login) {
		return login(login, Constants.Password);
	}
	
	public DepositPage login(String login, String password) {
		loginFld.sendKeys(login);
		passwordFld.sendKeys(password);
		loginBtn.click();

		return new DepositPage(driver);
	}

	public LoginPage set(String login, String password) {
		loginFld.sendKeys(login);
		passwordFld.sendKeys(password);
		
		return this;
	}
	
	public LoginPage clickLogin() {
		loginBtn.click();

		return this;
	}
	
	public String getError() {
		return errorMessage.getText();
	}
}