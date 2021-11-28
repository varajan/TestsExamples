package test.java.pages;

import java.util.ArrayList;
import java.util.List;

import org.openqa.selenium.*;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import test.java.data.Constants;

public class HistoryPage extends BasePage {
	@FindBy(xpath = "//div[text() = 'Calculator']")
	private WebElement calculator;

	@FindBy(id = "clear")
	private WebElement clear;

	@FindBy(id = "history")
	private WebElement history;
    		
	public HistoryPage(WebDriver driver) {
		super(driver);
		PageFactory.initElements(this.driver, this);

		try{
			new WebDriverWait(driver, Constants.WaitTimeout)
					.until(ExpectedConditions.numberOfElementsToBeMoreThan(By.xpath(".//tr[td]"), 0));
		} catch (Exception e){}
	}

	public DepositPage openCalculator() {
		calculator.click();
		
		return new DepositPage(driver);
	}

	public HistoryPage clear() {
		clear.click();
		
		return this;
	}

	public int getHistorySize() {
		return history.findElements(By.xpath(".//tr[td]")).size();
	}
	
	public List<String> getTableRow(int row){
		List<String> cells = new ArrayList<String>();
		WebElement tableRow = history.findElement(By.xpath(".//tr[td][" + row + "]"));

		for (WebElement cell : tableRow.findElements(By.xpath(".//td"))) {
			cells.add(cell.getText());
		}
		
		return cells;
	}
	
	public List<List<String>> getHistory(){
		List<List<String>> result = new ArrayList<List<String>>();
		
		for (int row = 1; row <= getHistorySize(); row++) {
			result.add(getTableRow(row));
		}
		
		return result;
	}
}