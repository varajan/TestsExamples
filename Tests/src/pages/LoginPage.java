package pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

import utilities.Constants;

public class LoginPage extends BasePage {
	@FindBy(id = "login")
	public WebElement loginFld;
	
	@FindBy(id = "password")
	public WebElement passwordFld;
	
	@FindBy(id = "loginBtn")
	public WebElement loginBtn;
	
	@FindBy(id = "errorMessage")
	public WebElement errorMessage;

	@FindBy(xpath = "//div[text() = 'Register']")
	private WebElement register;
	
	public LoginPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);
	}

	public RegisterPage openRegistration() {
		register.click();
		
		return new RegisterPage(driver);
	}
	
	public LoginPage deleteAllUsers() {
		driver.get(Constants.BaseUrl + "/Register/DeleteAllUsers");
		driver.get(Constants.BaseUrl);

		return this;
	}
	
	public DepositPage login() {
		return login(Constants.Login, Constants.Password);
	}
	
	public DepositPage login(String login, String password) {
		loginFld.sendKeys(login);
		passwordFld.sendKeys(password);
		loginBtn.click();

		return new DepositPage(driver);
	}
}
