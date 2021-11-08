package tests;

import org.junit.After;
import org.junit.Before;
import org.openqa.selenium.WebDriver;

import utilities.Driver;

public class BaseTest {
	public WebDriver driver;
	
	@Before
	public void initDriver() {
		driver = Driver.initWebDriver();
	}
	
	@After
    public void tearDown() {
        driver.quit();
    }
}
