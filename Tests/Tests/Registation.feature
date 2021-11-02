Feature: Registation

Background:
	Given Existed users:
	| Login  | Password | Email                   |
	| Milana | newyork1 | milana.Coleman@test.com |
	| Arnold | newyork1 | arnold@test.net         |

Scenario: Valid data
	Given I open Register page
	When I fill in registration data:
		| Login | Email             | Password | Password Confirm |
		| Steve | Steve@Collier.com | Meadows  | Meadows          |
	Then Registation is completed successfully with message: Registration was successful.
		And I can login with 'steve' login and 'Meadows' password

Scenario: Invalid data
	Given I open Register page
	When I fill in registration data:
		| Login   | Email   | Password    | Password Confirm |
		| <login> | <email> | <password1> | <password2>      |
	Then Registation is failed with message: <error>

Examples: 
		| login   | email              | password1 | password2 | error                                       |
		| Milana  | some.mail@test.com | password1 | password2 | Passwords are different!                    |
		| Milana  | some.mail@test.com | 1234      | 1234      | Password is too short.                      |
		| Kareena | arnold@test.net    | password1 | password1 | User with this email is already registered. |
		| Kareena | @test.net          | password1 | password1 | Invalid email.                              |
		| Kareena | mail.test.net      | password1 | password1 | Invalid email.                              |
