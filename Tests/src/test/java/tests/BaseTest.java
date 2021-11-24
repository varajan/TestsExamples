package test.java.tests;

import java.io.IOException;
import java.security.KeyManagementException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;

import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;
import test.java.data.Constants;
import test.java.data.Users;
import test.java.pages.DepositPage;
import test.java.pages.HistoryPage;
import test.java.pages.LoginPage;
import test.java.pages.SettingsPage;
import test.java.utilities.Driver;

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