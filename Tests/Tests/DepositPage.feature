Feature: DepositPage

Scenario: Default fiancial year is 365
	Given I am logged in
	When I open Deposit page
	Then Financial Year selected option is '365'

Scenario: Start Date day values
	Given I am logged in
	When I select <year> and <month> as Start Date
	Then I can select up to <days> as Day

	Examples: 
	| days | month     | year |
	| 31   | January   | 2021 |
	| 29   | February  | 2020 |
	| 28   | February  | 2021 |
	| 31   | March     | 2021 |
	| 30   | April     | 2021 |
	| 31   | May       | 2021 |
	| 30   | June      | 2021 |
	| 31   | July      | 2021 |
	| 31   | August    | 2021 |
	| 30   | September | 2021 |
	| 31   | October   | 2021 |
	| 30   | November  | 2021 |
	| 31   | December  | 2021 |

Scenario: Start Date month values
	Given I am logged in
	When I open Deposit page
	Then Start Date months have correct values

Scenario: Start Date year values
	Given I am logged in
	When I open Deposit page
	Then Start Date years have correct values

Scenario: Calculate End Date
	Given I am logged in
	When I select Start Date as '05/10/2022'
		And I calculate deposit for $1000 with 10% on 100 days
	Then End date is '08/02/2022'

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
