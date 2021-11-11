package tests;

import java.io.IOException;

import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;

import pages.*;
import utilities.Constants;
import utilities.Driver;
import utilities.Users;

public class BaseTest {
	private WebDriver driver;
	
	protected LoginPage loginPage;
	protected DepositPage depositPage;
	protected SettingsPage settingsPage;
	protected HistoryPage historyPage;
	
	@BeforeAll
	@AfterAll
	public static void beforeAll() throws IOException {
		Users.deleteAll();
		Users.register(Constants.Login);
	}
	
	@BeforeEach
	public void initDriver() throws IOException {
		driver = Driver.initWebDriver();
		loginPage = new LoginPage(driver);
	}
	
	@AfterEach
    public void tearDown() {
        driver.quit();
    }
}