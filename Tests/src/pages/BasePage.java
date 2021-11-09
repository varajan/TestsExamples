package pages;

import java.time.Duration;
import java.util.ArrayList;
import java.util.List;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;

public class BasePage {
	protected WebDriver driver;

	public BasePage(WebDriver driver) {
		this.driver = driver;
	}
	
	public String getTitle() {
		return driver.getTitle();
	}
	
	protected WebDriverWait driverWait() {
		return driverWait(3);
	}
	
	protected WebDriverWait driverWait(int seconds) {
		return new WebDriverWait(driver, Duration.ofSeconds(seconds));
	}
	
	protected String getSelectValue(WebElement element) {
		Select select = new Select(element);
		return select.getFirstSelectedOption().getText();
	}
	
	protected void setSelectValue(WebElement element, String value) {
		Select select = new Select(element);
		select.selectByVisibleText(value);
	}
	
	protected List<String> getSelectOptions(WebElement element){
		Select select = new Select(element);
		List<String> result = new ArrayList<String>();
		
		for (WebElement option : select.getOptions()) {
			result.add(option.getText());
		}
		
		return result;
	}
	
	protected String getInputValue(WebElement element) {
		return element.getAttribute("value");
	}

	protected void setInputValue(WebElement element, String value) {
		element.sendKeys(value);
	}
}
