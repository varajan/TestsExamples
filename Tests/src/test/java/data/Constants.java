package test.java.data;

public class Constants {
	public static int WaitTimeout = 3;

	public static String BaseUrl = System.getProperty("BaseUrl") != null
			? System.getProperty("BaseUrl")
			: "https://localhost:44392/";

	public static String Login = "test";
	public static String Password = "newyork1";

	public static String DefaultCurrency = "$ - US dollar";
	public static String DefaultDateFormat = "dd/MM/yyyy";
	public static String DefaultNumberFormat = "123,456,789.00";
}
