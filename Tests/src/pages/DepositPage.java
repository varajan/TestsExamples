package pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.PageFactory;

public class DepositPage {
	private WebDriver driver;
	
	public DepositPage(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(this.driver, this);
	}

	public Boolean isOpened() {
		return driver.getTitle().equals("Deposit calculator");
	}
}
