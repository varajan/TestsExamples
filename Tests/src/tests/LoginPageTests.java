package tests;

import org.junit.*;

import pages.DepositPage;
import pages.LoginPage;
import utilities.Constants;

public class LoginPageTests extends  BaseTest{

	@Test
	public void positiveLoginTest() {
		DepositPage depositPage = new LoginPage(driver).login();
		
		Assert.assertTrue(depositPage.isOpened());
	}

	@Test
	public void invalidLoginTest() {
		negativeLoginTest("text", Constants.Password);
	}

	@Test
	public void invalidPasswordTest() {
		negativeLoginTest(Constants.Login, "password");
	}
	
	@Test
	public void emptyLoginTest() {
		negativeLoginTest("", Constants.Password);
	}

	@Test
	public void emptyPasswordTest() {
		negativeLoginTest(Constants.Login, "");
	}

	@Test
	public void emptyLoginAndPasswordTest() {
		negativeLoginTest("", "");
	}
	
	private void negativeLoginTest(String login, String password) {
		LoginPage loginPage = new LoginPage(driver);
		
		loginPage.loginFld.sendKeys(login);
		loginPage.passwordFld.sendKeys(password);
		loginPage.loginBtn.click();
		
		Assert.assertEquals("Incorrect user name or password!", loginPage.errorMessage.getText());
	}
}
