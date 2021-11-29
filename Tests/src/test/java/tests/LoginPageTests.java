package test.java.tests;

import java.util.stream.Stream;

import org.junit.Assert;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import test.java.data.Constants;
import test.java.utilities.Result;

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
		loginPage.set(login, password).clickLogin();
		
		Assert.assertEquals("Incorrect user name or password!", loginPage.getError());
    }
	
	@Test
	public void openCloseRemindPasswordTest() {
		loginPage.remindPasswordView.open();
		loginPage.remindPasswordView.close();

		Assert.assertFalse(loginPage.remindPasswordView.isOpened());
	}
	
	static Stream<Arguments> remindPassword(){
	    return Stream.of(
 	       Arguments.of(false, "", "Invalid email."),
 	       Arguments.of(false, "@invalid.email", "Invalid email."),
 	       Arguments.of(false, "@invalid@email", "Invalid email."),
 	       Arguments.of(false, "invalid.email@", "Invalid email."),
 	       Arguments.of(false, "valid.email@test", "No user was found."),
 	       Arguments.of(true, "Test@test.com", "Email with instructions was sent to test@test.com")
		);
	}

	@ParameterizedTest
	@MethodSource("remindPassword")
	public void negativeRemindPasswordTest(boolean isSuccessful, String email, String error) {
		Result sendReminder = loginPage.remindPasswordView.open().send(email);
		
		Assertions.assertAll(
			() -> Assert.assertEquals(isSuccessful, sendReminder.isSuccessful()),
			() -> Assert.assertEquals(error, sendReminder.getMessage())
		);
	}
}
