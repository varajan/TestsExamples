package tests;

import java.util.Date;
import java.util.List;
import java.util.stream.Stream;

import org.junit.Assert;
import org.junit.Test;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import com.google.common.collect.Lists;

import utilities.Constants;
import utilities.Dates;

public class SettingsPageTests extends BaseTest {
	@BeforeEach
	public void openSettings() {
		settingsPage = loginPage.login().openSettings();
	}

	@Test
	public void verifyDropdownValues() {
		List<String> dateFormats   = Lists.newArrayList("dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "MM dd yyyy");
		List<String> numberFormats = Lists.newArrayList("123,456,789.00", "123.456.789,00", "123 456 789.00", "123 456 789,00");
		List<String> currencies    = Lists.newArrayList("$ - US dollar", "€ - euro", "£ - Great Britain Pound");

		Assertions.assertAll(
			() -> Assert.assertEquals(dateFormats, settingsPage.getDateFormats()),
			() -> Assert.assertEquals(numberFormats, settingsPage.getNumberFormats()),
			() -> Assert.assertEquals(currencies, settingsPage.getCurrencies())
		);
	}

	static Stream<Arguments> currencies(){
	    return Stream.of(
	       Arguments.of("$", "$ - US dollar"),
	       Arguments.of("€", "€ - euro"),
	       Arguments.of("£", "£ - Great Britain Pound")
	    );
	}

	@ParameterizedTest
	@MethodSource("currencies")
	public void changeCurrencyTest(String symbol, String currency)
    {
        settingsPage.setCurrency(currency);
        depositPage = settingsPage.save();

        Assert.assertEquals(symbol, depositPage.getCurrency());
    }

	static Stream<Arguments> dateFormats(){
	    return Stream.of(
	 	       Arguments.of("dd/MM/yyyy"),
		       Arguments.of("dd-MM-yyyy"),
		       Arguments.of("MM/dd/yyyy"),
		       Arguments.of("MM dd yyyy")
	    );
	}

	@ParameterizedTest
	@MethodSource("dateFormats")
	public void changeDateFormatsTest(String format)
    {
		String expectedDate = Dates.formatDate(new Date(), format);

		settingsPage.setDateFormat(format);
        depositPage = settingsPage.save();

        Assert.assertEquals(expectedDate, depositPage.getEndDate());
    }

	static Stream<Arguments> numberFormats(){
	    return Stream.of(
	 	       Arguments.of("123,456,789.00", "127,397.26", "27,397.26"),
		       Arguments.of("123.456.789,00", "127.397,26", "27.397,26"),
		       Arguments.of("123 456 789.00", "127 397.26", "27 397.26"),
		       Arguments.of("123 456 789,00", "127 397,26", "27 397,26")
	    );
	}

	@ParameterizedTest
	@MethodSource("numberFormats")
	public void changeNumberFormatsTest(String format, String income, String interest)
    {
		settingsPage.setNumberFormat(format);
        depositPage = settingsPage.save();

        depositPage.populate("100000", "100", "100");
        depositPage.calculate();
        
		Assertions.assertAll(
	        () -> Assert.assertEquals(income, depositPage.getIncome()),
	        () -> Assert.assertEquals(interest, depositPage.getInterest())
        );
    }
	
	@Test
	public void LogoutTest() {
		loginPage = settingsPage.logout();
		
		Assert.assertEquals("Login", loginPage.getTitle());
	}
	
	@Test
	public void CancelSettingsChangesTest()
    {
		settingsPage
			.set("MM/dd/yyyy", "123 456 789,00", "€ - euro")
			.cancel()
			.openSettings();

		Assertions.assertAll(
			() -> Assert.assertEquals(Constants.DefaultDateFormat, settingsPage.getDateFormat()),
			() -> Assert.assertEquals(Constants.DefaultNumberFormat, settingsPage.getNumberFormat()),
			() -> Assert.assertEquals(Constants.DefaultCurrency, settingsPage.getCurrency())
		);
    }
}
