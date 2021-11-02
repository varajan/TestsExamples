Feature: Login

Background:
	Given Existed users:
	| Login | Password | Email         |
	| Brad  | newyork1 | Brad@test.com |
	| Finn  | Newyork9 | Finn@test.net |

Scenario: Login with correct credentials
	Given I open Login page
	When I login with 'Brad' login and 'newyork1' password
	Then Deposit calculator page is opened

Scenario: Login with incorrect credentials
	Given I open Login page
	When I login with '<login>' login and '<password>' password
	Then '<error>' error is shown

	Examples: 
	| login | password | error                                   |
	| Brad  | newyork2 | Incorrect user name or password!        |
	| Brad  | newyork9 | Incorrect user name or password!        |
	| Brod  | newyork1 | Incorrect user name or password!        |
	| Finn  | newyork1 | Incorrect user name or password!        |
	|       | newyork9 | Incorrect user name or password!        |
	| Brad  |          | Incorrect user name or password!        |
	|       |          | Incorrect user name or password!        |

