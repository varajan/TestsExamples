package tests;

import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;

import pages.DepositPage;
import pages.LoginPage;
import pages.SettingsPage;
import utilities.Driver;

public class BaseTest {
	private WebDriver driver;
	
	protected SettingsPage settingsPage;
	protected DepositPage depositPage;
	protected LoginPage loginPage;
	
	@BeforeEach
	public void initDriver() {
		driver = Driver.initWebDriver();
		loginPage = new LoginPage(driver);
	}
	
	@AfterEach
    public void tearDown() {
        driver.quit();
    }
}