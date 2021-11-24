package pages;

import java.util.ArrayList;
import java.util.List;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Select;

public class BasePage extends PageWaits {
	public BasePage(WebDriver driver) {
		super(driver);
	}
	
	public String getTitle() {
		return driver.getTitle();
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
		element.clear();
		element.sendKeys(value);
	}
}
