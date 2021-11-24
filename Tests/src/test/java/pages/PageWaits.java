package test.java.pages;

import java.time.Duration;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import test.java.data.Constants;

public abstract class PageWaits {
	protected WebDriver driver;
	
	public PageWaits(WebDriver driver) {
		this.driver = driver;
	}

	protected void waitForElementVisible(WebElement element) {
		driverWait(Constants.WaitTimeout).until(ExpectedConditions.visibilityOf(element));
	}

	protected void waitForElementToBeClickable(WebElement element) {
		driverWait(Constants.WaitTimeout).until(ExpectedConditions.elementToBeClickable(element));
	}
	
	protected void waitForAlertOrElementVisible(By locator) {
		try {
			driverWait(Constants.WaitTimeout)
				.until(ExpectedConditions.or(
						ExpectedConditions.alertIsPresent(),
						ExpectedConditions.visibilityOfElementLocated(locator)));
		} catch (Exception e) {
			// nothing
		}
	}
	
	protected void waitForAlert() {
		driverWait(Constants.WaitTimeout).until(ExpectedConditions.alertIsPresent());
	}
	
	protected boolean isAlertShown() {
		try {
			return driverWait(0).until(ExpectedConditions.alertIsPresent()) != null;
		} catch (Exception e) {
			return false;
		}
	}
	
	protected WebDriverWait driverWait(int seconds) {
		return new WebDriverWait(driver, Duration.ofSeconds(seconds));
	}
}