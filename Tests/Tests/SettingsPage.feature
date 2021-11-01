Feature: SettingsPage

Scenario: Available date formats
	Given I am logged in
	When I open Settings page
	Then Available date formats are:
	| Format     |
	| dd/MM/yyyy |
	| dd-MM-yyyy |
	| MM/dd/yyyy |
	| MM dd yyyy |

Scenario: Available number formats
	Given I am logged in
	When I open Settings page
	Then Available number formats are:
	| Format         |
	| 123,456,789.00 |
	| 123.456.789,00 |
	| 123 456 789.00 |
	| 123 456 789,00 |

Scenario: Available currencies
	Given I am logged in
	When I open Settings page
	Then Available currencies are:
	| Format                  |
	| $ - US dollar           |
	| € - euro                |
	| £ - Great Britain Pound |

Scenario: Logout
	Given I am logged in
	When I open Settings page
		And I click Logout button
	Then Login page is opened
