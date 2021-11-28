Feature: StartDateDay

Scenario: Start Date day values
	Given I login as 'Avani'
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
