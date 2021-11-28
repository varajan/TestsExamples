package test.java.tests;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.stream.Stream;

import org.junit.Assert;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import com.google.common.collect.Lists;

public class DepositPageTests extends BaseTest {
	@BeforeEach
	public void login() {
		depositPage = loginPage.login();
	}
	
	@Test
	public void verifyDefaultFinYearTest() {
		Assert.assertEquals("365", depositPage.getFinYear());
	}

	static Stream<Arguments> dayMonthYear(){
	    return Stream.of(
	 	       Arguments.of("January", "2020", 31),
		       Arguments.of("February", "2020", 29),
		       Arguments.of("February", "2021", 28),
		       Arguments.of("March", "2020", 31),
		       Arguments.of("April", "2020", 30),
		       Arguments.of("May", "2020", 31),
		       Arguments.of("June", "2020", 30),
		       Arguments.of("July", "2020", 31),
		       Arguments.of("August", "2020", 31),
		       Arguments.of("September", "2020", 30),
		       Arguments.of("October", "2020", 31),
		       Arguments.of("November", "2020", 30),
		       Arguments.of("December", "2020", 31)
		);
	}

	@ParameterizedTest
	@MethodSource("dayMonthYear")
	public void verifyDayValuesTest(String month, String year, int daysInMonth) {
		List<String> days = new ArrayList<String>();
		for (int day = 1; day <= daysInMonth; day++) {
			days.add(String.valueOf(day));
		}

		depositPage.setYear(year);
		depositPage.setMonth(month);
		
		Assert.assertEquals(days, depositPage.getDays());
	}

	@Test
	public void verifyMonthValuesTest() {
		List<String> months = Lists.newArrayList("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");

		Assert.assertEquals(months, depositPage.getMonths());
	}

	@Test
	public void verifyYearValuesTest() {
		List<String> years = new ArrayList<String>();
		for (int year = 2010; year < 2030; year++) {
			years.add(String.valueOf(year));
		}
		
		Assert.assertEquals(years, depositPage.getYears());
	}
	
	@Test
	public void verifyDefaultStartDate() throws ParseException {
		String today = new SimpleDateFormat("dd/MM/yyyy").format(new Date());
		
		Assert.assertEquals(today, depositPage.getStartDate());
	}
	
	@Test
    public void calculateEndDateTest() throws ParseException
    {
        depositPage.populate("1000", "10", "100");
        depositPage.setStartDate("21/07/2022");
        depositPage.calculate();

        Assert.assertEquals("29/10/2022", depositPage.getEndDate());
    }

	static Stream<Arguments> calculateDeposit(){
	    return Stream.of(
			Arguments.of("6000", "10", "120", "360", "200.00", "6,200.00"),
			Arguments.of("6000", "10", "120", "365", "197.26", "6,197.26"),
			Arguments.of("100000", "99.9", "365", "365", "99,900.00", "199,900.00"),
			Arguments.of("100000", "100.0", "360", "360", "100,000.00", "200,000.00")
		);
	}

	@ParameterizedTest
	@MethodSource("calculateDeposit")
	public void calculateDepositTest(String amount, String percent, String term, String finYear, String interest, String income) {
		depositPage.populate(amount, percent, term);
		depositPage.setFinYear(finYear);
		depositPage.calculate();
		
		Assertions.assertAll(
			() -> Assert.assertEquals(interest, depositPage.getInterest()),
			() -> Assert.assertEquals(income, depositPage.getIncome())
		);
	}

	static Stream<Arguments> allowedAmountValues(){
	    return Stream.of(
			Arguments.of("100000", "100000"),
			Arguments.of("99999.99", "99999.99"),
			Arguments.of("100001", "0")
		);
	}

	@ParameterizedTest
	@MethodSource("allowedAmountValues")
	public void allowedAmountValuesTest(String enteredAmount, String displayedAmount)
    {
		depositPage.setAmount(enteredAmount);
		
		Assert.assertEquals(displayedAmount, depositPage.getAmount());
    }

	static Stream<Arguments> allowedInterestValues(){
	    return Stream.of(
			Arguments.of("100", "100"),
			Arguments.of("99.99", "99.99"),
			Arguments.of("100.1", "0")
		);
	}

	@ParameterizedTest
	@MethodSource("allowedInterestValues")
	public void allowedInterestValuesTest(String enteredAmount, String displayedAmount) {
		depositPage.setPercent("100");
		
		Assert.assertEquals("100", depositPage.getPercent());
	}

	static Stream<Arguments> verifyTermValues(){
	    return Stream.of(
				Arguments.of("360", "360", "360"),
				Arguments.of("360", "361", "0"),
				Arguments.of("365", "360", "360"),
				Arguments.of("365", "366", "0"),
				Arguments.of("365", "3.5", "0")
		);
	}

	@ParameterizedTest
	@MethodSource("verifyTermValues")
	public void verifyTermTest(String finYear, String entered, String displayed) {
		depositPage.setFinYear(finYear);
		depositPage.setTerm(entered);
		
		Assert.assertEquals(displayed, depositPage.getTerm());
	}
}
