package utilities;

import java.time.Duration;

import org.openqa.selenium.UnexpectedAlertBehaviour;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;

public class Driver {
	public static WebDriver initWebDriver() {
		System.setProperty("webdriver.chrome.driver", "chromedriver.exe");

		ChromeOptions options = new ChromeOptions();
		options.setUnhandledPromptBehaviour(UnexpectedAlertBehaviour.IGNORE);

		WebDriver driver = new ChromeDriver(options);

		driver.get(Constants.BaseUrl);
		driver.manage().timeouts().pageLoadTimeout(Duration.ofSeconds(Constants.WaitTimeout));
		driver.manage().timeouts().implicitlyWait(Duration.ofSeconds(Constants.WaitTimeout));

		return driver;
	}
}
