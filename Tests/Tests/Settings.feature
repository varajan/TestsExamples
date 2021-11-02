Feature: Settings

Scenario: Cancel settings changes
	Given I login as 'John'
		And I restore setting to default values
	When I open Settings page
		And I set currency to € - euro
		And I set number format to 123 456 789,00
		And I set dates format to MM/dd/yyyy
		But I click Cancel button
		And I open Settings page
	Then Settings have default values

Scenario: Apply number format
	Given I login as 'John'
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
	Given I login as 'John'
		And I open Settings page
		And I set dates format to <format>
		And I save changes
	When I select Start Date as '05/10/2022'
		And I calculate deposit for $1000 with 10% on 100 days
	Then End date is '<date>'

Examples: 
	| format     | date       |
	| dd/MM/yyyy | 18/08/2022 |
	| dd-MM-yyyy | 18-08-2022 |
	| MM/dd/yyyy | 08/18/2022 |
	| MM dd yyyy | 08 18 2022 |


Scenario: Apply currency
	Given I login as '<user>'
		And I open Settings page
	When I set currency to <currency>
		And I save changes
	Then <code> currency is shown

Examples: 
	| user      | currency                | code |
	| Christina | $ - US dollar           | $    |
	| Chantelle | € - euro                | €    |
	| Teodor    | £ - Great Britain Pound | £    |
