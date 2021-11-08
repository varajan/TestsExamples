package utilities;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class Dates {
	@SuppressWarnings("deprecation")
	public static int getMonthNumberFromName(String month) throws ParseException {
		return new SimpleDateFormat("MMMM", Locale.ENGLISH)
				.parse(month)
				.getMonth()+1;
	}
	
	public static String formatDate(String value, String format) throws ParseException {
		Date date = new SimpleDateFormat("dd/MM/yyyy").parse(value);
		String result = new SimpleDateFormat(format, Locale.ENGLISH).format(date);

		return result;
	}
}
