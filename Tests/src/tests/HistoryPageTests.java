package tests;

import java.io.IOException;
import java.text.ParseException;
import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.Stream;

import org.junit.Assert;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import utilities.Users;

public class HistoryPageTests extends BaseTest {
	@BeforeEach
	public void openDepositPage() throws IOException {
		String user = "settings";

		Users.delete(user);
		Users.register(user);

		depositPage = loginPage.login(user);
	}
	
	@Test
	public void clearHistoryTest() {
		depositPage.populate("1000", "10", "299").calculate();
		depositPage.populate("1250", "25", "199").calculate();
		
		historyPage = depositPage
			.openHistory()
			.clear()
			.openCalculator()
			.openHistory();
		
		Assert.assertEquals(0, historyPage.getHistorySize());
	}
	
	@Test
	public void assertHistoryTest() throws ParseException {
		List<String> first  = depositPage.populate("1000", "10", "299").calculate().getData();
		List<String> second = depositPage.populate("1250", "25", "199").calculate().getData();
		
		historyPage = depositPage.openHistory();
		
		Assertions.assertAll(
			() -> Assert.assertEquals(2,      historyPage.getHistorySize()),	
			() -> Assert.assertEquals(first,  historyPage.getTableRow(2)),
			() -> Assert.assertEquals(second, historyPage.getTableRow(1))
		);
	}

	@Test
	public void historyShowsLast9RecordsTest() throws ParseException
    {
        var expectedHistory = new ArrayList<List<String>>();

        for (var i = 1; i < 12; i++)
        {
    		List<String> row = depositPage.populate("1000", String.valueOf(i), "299").calculate().getData();
    		expectedHistory.add(0, row);
        }
        
        historyPage = depositPage.openHistory();

        expectedHistory = (ArrayList<List<String>>) expectedHistory.stream().limit(9).collect(Collectors.toList());
        
        Assert.assertEquals(expectedHistory, historyPage.getHistory());
    }

	static Stream<Arguments> settings(){
	    return Stream.of(
	       Arguments.of("$ - US dollar", "123.456.789,00", "dd-MM-yyyy"),
	       Arguments.of("€ - euro", "123 456 789.00", "MM/dd/yyyy"),
	       Arguments.of("£ - Great Britain Pound", "123 456 789,00", "MM dd yyyy")
	    );
	}

	@ParameterizedTest
	@MethodSource("settings")
	public void historyRespectSettingsTest(String currency, String numberFormat, String dateFormat) throws ParseException {
		depositPage.openSettings().set(dateFormat, numberFormat, currency).save();
		
		List<String> first  = depositPage.populate("100000", "99", "299").calculate().getData(dateFormat, numberFormat);
		List<String> second = depositPage.populate("100000", "75", "299").calculate().getData(dateFormat, numberFormat);

		historyPage = depositPage.openHistory();
		
		Assertions.assertAll(
			() -> Assert.assertEquals(2,      historyPage.getHistorySize()),	
			() -> Assert.assertEquals(first,  historyPage.getTableRow(2)),
			() -> Assert.assertEquals(second, historyPage.getTableRow(1))
		);
	}
}