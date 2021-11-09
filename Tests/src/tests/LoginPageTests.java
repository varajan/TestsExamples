package tests;

import java.util.stream.Stream;

import org.junit.*;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import utilities.Constants;

public class LoginPageTests extends  BaseTest{
	@Test
	public void positiveLoginTest() {
		depositPage = loginPage.login();
		
		Assert.assertEquals("Deposit calculator", depositPage.getTitle());
	}

	static Stream<Arguments> credentials(){
	    return Stream.of(
	 	       Arguments.of(Constants.Login + ".", Constants.Password),
		       Arguments.of(Constants.Login, Constants.Password + "."),
		       Arguments.of("", Constants.Password),
		       Arguments.of(Constants.Login, ""),
		       Arguments.of("", "")
		);
	}

	@ParameterizedTest
	@MethodSource("credentials")
	public void negativeLoginTest(String login, String password)
    {
		loginPage.loginFld.sendKeys(login);
		loginPage.passwordFld.sendKeys(password);
		loginPage.loginBtn.click();
		
		Assert.assertEquals("Incorrect user name or password!", loginPage.errorMessage.getText());
    }
}
