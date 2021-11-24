package test.java.tests;

import java.util.stream.Stream;

import org.junit.Assert;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

public class RegisterPageTests extends BaseTest {
	@Test
	public void positiveRegistrationTest() {
		String login = "LoGin";
		String password = "Password";
		String email = "login@test.com";
		
		String pageTitle = loginPage
			.openRegistration()
			.register(login.toLowerCase(), email, password)
			.login(login, password)
			.getTitle();
		
		Assert.assertEquals("Deposit calculator", pageTitle);
	}
	
	static Stream<Arguments> credentials(){
	    return Stream.of(
 	       Arguments.of("Test", "password", "password", "some-email@test.com", "User with this login is already registered."),
 	       Arguments.of("User", "password", "password", "test@test.com", "User with this email is already registered."),
 	       Arguments.of("User", "password", "passwort", "some-email@test.com", "Passwords are different!"),
 	       Arguments.of("User", "password", "password", "@test.com", "Invalid email."),
 	       Arguments.of("User", "password", "password", "some.test.com", "Invalid email."),
 	       Arguments.of("User", "pass", "pass", "some@test.com", "Password is too short.")
       );
	}
	
	@ParameterizedTest
	@MethodSource("credentials")
	public void negativeRegistrationTest(String login, String password1, String password2, String email, String error) {
		String registerError = loginPage
			.openRegistration()
			.tryRegister(login, email, password1, password2);
		
		Assert.assertEquals(error, registerError);
	}
}