package test.java.pages;

import org.openqa.selenium.Alert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import test.java.utilities.Result;

public class RemindPasswordView extends PageWaits {
	private String remindPasswordView = "remindPasswordView";
	
	private WebElement getView() {
		driver.switchTo().parentFrame();
		return driver.findElement(By.id(remindPasswordView));
	}

	public RemindPasswordView(WebDriver driver) {
		super(driver);
	}

	public Result send(String email) {
		String resultMessage;
		By message = By.id("message");
		driver.switchTo().frame(remindPasswordView);
		driver.findElement(By.id("email")).sendKeys(email);
		driver.findElement(By.xpath("//button[text() = 'Send']")).click();

		waitForAlertOrElementVisible(message);

		boolean isAlertShown = isAlertShown();

		if (isAlertShown) {
			Alert alert = driver.switchTo().alert();
			resultMessage = alert.getText();
			alert.accept();
		} else {
			resultMessage = driver.findElement(message).getText();
		}

		driver.switchTo().parentFrame();
		
		return new Result(isAlertShown, resultMessage);
	}
	
	public void close() {
		driver.switchTo().frame(remindPasswordView);
		driver.findElement(By.xpath("//button[text() = 'x']")).click();
		driver.switchTo().parentFrame();
	}
	
	public RemindPasswordView open() {
		if (!isOpened()) {
			driver.findElement(By.id("remindBtn")).click();
		}
		
		return this;
	}
	
	public boolean isOpened() {
		driver.switchTo().parentFrame();
		return getView().isDisplayed();
	}
}