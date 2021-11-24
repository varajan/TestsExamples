package test.java.utilities;

import java.time.Duration;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.UnexpectedAlertBehaviour;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import test.java.data.Constants;

public class Driver {
	public static WebDriver initWebDriver() {
		WebDriverManager.chromedriver().setup();
//		driver = new ChromeDriver();
//		System.setProperty("webdriver.chrome.driver", "chromedriver");

		ChromeOptions options = new ChromeOptions();
		options.setUnhandledPromptBehaviour(UnexpectedAlertBehaviour.IGNORE);
		options.setAcceptInsecureCerts(true);
		options.addArguments("--headless");
		options.addArguments("--no-sandbox");
		options.addArguments("--disable-dev-shm-usage");

		WebDriver driver = new ChromeDriver(options);

		driver.get(Constants.BaseUrl);
		driver.manage().timeouts().pageLoadTimeout(Duration.ofSeconds(Constants.WaitTimeout));
		driver.manage().timeouts().implicitlyWait(Duration.ofSeconds(Constants.WaitTimeout));

		return driver;
	}
}
