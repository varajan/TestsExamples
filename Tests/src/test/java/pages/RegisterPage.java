package test.java.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class RegisterPage extends BasePage {
	@FindBy(id = "login")
	private WebElement login;

	@FindBy(id = "email")
	private WebElement email;

	@FindBy(id = "password1")
	private WebElement password1;

	@FindBy(id = "password2")
	private WebElement password2;

	@FindBy(id = "errorMessage")
	private WebElement errorMessage;

	@FindBy(id = "register")
	private WebElement register;

	public RegisterPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);
	}
	
	public String tryRegister(String login, String email, String password1, String password2) {
		this.login.sendKeys(login);
		this.email.sendKeys(email);
		this.password1.sendKeys(password1);
		this.password2.sendKeys(password2);
		this.register.click();
		
		waitForElementVisible(errorMessage);

		return this.errorMessage.getText();
	}
	
	public LoginPage register(String login, String email, String password) {
		this.login.sendKeys(login);
		this.email.sendKeys(email);
		this.password1.sendKeys(password);
		this.password2.sendKeys(password);
		this.register.click();
		
		waitForAlert();
		driver.switchTo().alert().accept();
		
		return new LoginPage(driver);
	}
}