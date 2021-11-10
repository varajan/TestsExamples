package utilities;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.Locale;

public class Strings {
	public static String formatNumber(String number, String format)
    {
		DecimalFormatSymbols symbols = new DecimalFormatSymbols(Locale.US);
		Double value = Double.valueOf(number);
        
        switch (format)
        {
        	case "123,456,789.00":
                symbols.setGroupingSeparator(',');
                symbols.setDecimalSeparator('.');
                break;

        	case "123.456.789,00":
        		symbols.setGroupingSeparator('.');
                symbols.setDecimalSeparator(',');
            	break;

            case "123 456 789.00":
            	symbols.setGroupingSeparator(' ');
                symbols.setDecimalSeparator('.');
                break;

            case "123 456 789,00":
            	symbols.setGroupingSeparator(' ');
                symbols.setDecimalSeparator(',');
                break;
        }
        
		return new DecimalFormat("#,###.00", symbols).format(value);
    }
}
