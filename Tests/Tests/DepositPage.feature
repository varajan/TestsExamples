Feature: DepositPage

Scenario: Default fiancial year is 365
	Given I am logged in
	When I open Deposit page
	Then Financial Year selected option is '365'

Scenario: Start Date month values
	Given I am logged in
	When I open Deposit page
	Then Start Date months have correct values

Scenario: Start Date year values
	Given I am logged in
	When I open Deposit page
	Then Start Date years have correct values

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
