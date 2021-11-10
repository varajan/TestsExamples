package tests;

import org.junit.*;
import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;

import pages.*;
import utilities.Driver;

public class BaseTest {
	private WebDriver driver;
	
	protected LoginPage loginPage;
	protected DepositPage depositPage;
	protected SettingsPage settingsPage;
	protected HistoryPage historyPage;
	
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