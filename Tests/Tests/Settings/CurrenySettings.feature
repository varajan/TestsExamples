Feature: Currency Settings

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
	| Teodor    | ₴ - Ukrainian hryvnia   | ₴    |
