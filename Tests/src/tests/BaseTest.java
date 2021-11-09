package tests;

import org.junit.After;
import org.junit.Before;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
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
	
	@Before
	@BeforeEach
	public void initDriver() {
		driver = Driver.initWebDriver();
		loginPage = new LoginPage(driver);
	}
	
	@After
	@AfterEach
    public void tearDown() {
        driver.quit();
    }
}