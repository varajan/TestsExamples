Feature: DepositPage

Scenario: Default fiancial year is 365
	Given I login as 'Karl'
	When I open Calculator page
	Then Financial Year selected option is '365'

Scenario: Start Date month values
	Given I login as 'Karl'
	When I open Calculator page
	Then Start Date months have correct values

Scenario: Start Date year values
	Given I login as 'Karl'
	When I open Calculator page
	Then Start Date years have correct values
