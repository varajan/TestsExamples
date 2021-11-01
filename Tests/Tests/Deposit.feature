Feature: Deposit

Background:
	Given Setting have default values

Scenario: Calculate End Date
	Given I am logged in
	When I select Start Date as '05/10/2022'
		And I calculate deposit for $1000 with 10% on 100 days
	Then End date is '18/08/2022'

Scenario: Calculate deposit
	Given I am logged in
	When I calculate deposit for <amount> with <percent> on <term> days with <finYear> financial year
	Then Interest is <interest>
		And Income is <income>

Examples: 
	| amount | percent | term | finYear | interest   | income     |
	| 6000   | 10      | 120  | 360     | 200.00     | 6,200.00   |
	| 6000   | 10      | 120  | 365     | 197.26     | 6,197.26   |
	| 100000 | 99.9    | 365  | 365     | 99,900.00  | 199,900.00 |
	| 100000 | 100.0   | 360  | 360     | 100,000.00 | 200,000.00 |
