﻿Feature: SettingsPage

Scenario: Logout
	Given I am logged in
	When I open Settings page
		And I click Logout button
	Then Login page is opened

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

Scenario: Cancel settings changes
	Given I am logged in
		And I restore setting to default values
	When I open Settings page
		And I set currency to € - euro
		And I set number format to 123 456 789,00
		And I set dates format to MM/dd/yyyy
		But I click Cancel button
		And I open Settings page
	Then Settings have default values

Scenario: Apply number format
	Given I am logged in
		And I open Settings page
		And I set number format to <format>
		And I save changes
	When I calculate deposit for $100000 with 100% on 100 days
	Then Income is <income>
		And Interest is <interest>

Examples:
| format         | income     | interest  |
| 123,456,789.00 | 127,397.26 | 27,397.26 |
| 123.456.789,00 | 127.397,26 | 27.397,26 |
| 123 456 789.00 | 127 397.26 | 27 397.26 |
| 123 456 789,00 | 127 397,26 | 27 397,26 |


Scenario: Apply dates format
	Given I am logged in
		And I open Settings page
		And I set dates format to <format>
		And I save changes
	When I select Start Date as '05/10/2022'
		And I calculate deposit for $1000 with 10% on 100 days
	Then End date is '<date>'

Examples: 
| format     | date       |
| dd/MM/yyyy | 08/02/2022 |
| dd-MM-yyyy | 08-02-2022 |
| MM/dd/yyyy | 02/08/2022 |
| MM dd yyyy | 02 08 2022 |


Scenario: Apply currency
	Given I am logged in
		And I open Settings page
	When I set currency to <currency>
		And I save changes
	Then <code> currency is shown

Examples: 
| currency                | code |
| $ - US dollar           | $    |
| € - euro                | €    |
| £ - Great Britain Pound | £    |
