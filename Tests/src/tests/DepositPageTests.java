package tests;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import com.google.common.collect.Lists;

import pages.DepositPage;
import pages.LoginPage;

public class DepositPageTests extends BaseTest {
	private DepositPage depositPage;
	
	@Before
	public void login() {
		depositPage	= new LoginPage(driver).login();
	}
	
	@Test
	public void verifyDefaultFinYearTest() {
		Assert.assertEquals("365", depositPage.getFinYear());
	}

	@Test
	public void verifyDayValuesInJanuary2020() {
		verifyDayValues("January", "2020", 31);
	}
	
	@Test
	public void verifyDayValuesInFebruary2020() {
		verifyDayValues("February", "2020", 29);
	}
	
	@Test
	public void verifyDayValuesInFebruary2021() {
		verifyDayValues("February", "2021", 28);
	}
	
	private void verifyDayValues(String month, String year, int daysInMonth) {
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
		for (int year = 2010; year <= 2025; year++) {
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

	@Test
	public void calculateDepositWtith365Test() {
		calculateDeposit("100000", "99.9", "365", "365", "99,900.00", "199,900.00");	
	}

	@Test
	public void calculateDepositWith360Test() {
		calculateDeposit("100000", "100.0", "360", "360", "100,000.00", "200,000.00");	
	}
	
	private void calculateDeposit(String amount, String percent, String term, String finYear, String interest, String income) {
		depositPage.populate(amount, percent, term);
		depositPage.setFinYear(finYear);
		depositPage.calculate();
		
		Assert.assertEquals(interest, depositPage.getInterest());
		Assert.assertEquals(income, depositPage.getIncome());
	}

	@Test
	public void VerifyCanEnter100_000AmountTest() {
		depositPage.setAmount("100000");
		Assert.assertEquals("100000", depositPage.getAmount());
	}

	@Test
	public void VerifyCannotEnter100_001AmountTest() {
		depositPage.setAmount("100001");
		Assert.assertEquals("0", depositPage.getAmount());
	}
	
	@Test
	public void VerifyCanEnter100PercentTest() {
		depositPage.setPercent("100");
		Assert.assertEquals("100", depositPage.getPercent());
	}

	@Test
	public void VerifyCanEnter99_9PercentTest() {
		depositPage.setPercent("99.9");
		Assert.assertEquals("99.9", depositPage.getPercent());
	}

	@Test
	public void VerifyCannotEnter100_1PercentTest() {
		depositPage.setPercent("100.1");
		Assert.assertEquals("0", depositPage.getPercent());
	}

	@Test
	public void VerifyMaxTerm360DaysTest() {
		VerifyTerm("360", "360", "360");
	}

	@Test
	public void VerifyMaxTerm361DaysTest() {
		VerifyTerm("360", "361", "0");
	}

	@Test
	public void VerifyMaxTerm365DaysTest() {
		VerifyTerm("365", "365", "365");
	}

	@Test
	public void VerifyMaxTerm366DaysTest() {
		VerifyTerm("365", "366", "0");
	}

	@Test
	public void VerifyFloatTermTest() {
		VerifyTerm("365", "3.6", "0");
	}

	private void VerifyTerm(String finYear, String entered, String displayed) {
		depositPage.setFinYear(finYear);
		depositPage.setTerm(entered);
		Assert.assertEquals(displayed, depositPage.getTerm());
	}
}
