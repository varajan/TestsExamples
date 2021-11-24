package tests;

import java.io.IOException;
import java.security.KeyManagementException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;

import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;

import pages.*;
import data.Constants;
import utilities.Driver;
import data.Users;

public class BaseTest {
	private WebDriver driver;
	
	protected LoginPage loginPage;
	protected DepositPage depositPage;
	protected SettingsPage settingsPage;
	protected HistoryPage historyPage;
	
	@BeforeAll
	@AfterAll
	public static void beforeAll() throws IOException, NoSuchAlgorithmException, KeyStoreException, KeyManagementException {
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