Feature: Date Format Settings

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
