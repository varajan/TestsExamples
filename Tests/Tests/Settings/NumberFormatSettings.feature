Feature: Number Format Settings

Scenario: Apply number format
	Given I login as '<user>'
		And I open Settings page
		And I set number format to <format>
		And I save changes
	When I calculate deposit for $100000 with 100% on 100 days
	Then Income is <income>
		And Interest is <interest>

Examples:
	| user    | format         | income     | interest  |
	| Jarvis  | 123,456,789.00 | 127,397.26 | 27,397.26 |
	| Harlan  | 123.456.789,00 | 127.397,26 | 27.397,26 |
	| Raymond | 123 456 789.00 | 127 397.26 | 27 397.26 |
	| Timothy | 123 456 789,00 | 127 397,26 | 27 397,26 |
