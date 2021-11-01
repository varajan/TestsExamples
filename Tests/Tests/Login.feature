Feature: Login

Scenario: Login with correct credentials
	Given I open Login page
	When I login with 'test' login and 'newyork1' password
	Then Deposit calculator page is opened

Scenario: Login with incorrect credentials
	Given I open Login page
	When I login with '<login>' login and '<password>' password
	Then '<error>' error is shown

	Examples: 
	| login | password | error                                   |
	| test  | newyork2 | Incorrect user name or password!        |
	| text  | newyork1 | Incorrect user name or password!        |
	|       | newyork1 | User name and password cannot be empty! |
	| test  |          | User name and password cannot be empty! |
	|       |          | User name and password cannot be empty! |

