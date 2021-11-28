Feature: Settings

Scenario: Cancel settings changes
	Given I login as 'Kaydon'
		And I restore setting to default values
	When I open Settings page
		And I set currency to € - euro
		And I set number format to 123 456 789,00
		And I set dates format to MM/dd/yyyy
		But I click Cancel button
		And I open Settings page
	Then Settings have default values

Scenario: Available date formats
	Given I login as 'Kaydon'
	When I open Settings page
	Then Available date formats are:
	| Format     |
	| dd/MM/yyyy |
	| dd-MM-yyyy |
	| MM/dd/yyyy |
	| MM dd yyyy |

Scenario: Available number formats
	Given I login as 'Kaydon'
	When I open Settings page
	Then Available number formats are:
	| Format         |
	| 123,456,789.00 |
	| 123.456.789,00 |
	| 123 456 789.00 |
	| 123 456 789,00 |

Scenario: Available currencies
	Given I login as 'Kaydon'
	When I open Settings page
	Then Available currencies are:
	| Format                  |
	| $ - US dollar           |
	| € - euro                |
	| £ - Great Britain Pound |
	| ₴ - Ukrainian hryvnia   |

Scenario: Logout
	Given I login as 'Kaydon'
	When I open Settings page
		And I click Logout button
	Then Login page is opened
