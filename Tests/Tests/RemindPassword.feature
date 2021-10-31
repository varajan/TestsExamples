Feature: RemindPassword

Scenario: Close Remind Password
	Given I open Login page
		And I click Remind Password button
	When I click 'x' button
	Then Remind Password is closed

Scenario: Remind Password with valid user
	Given I open Login page
		And I click Remind Password button
	When I send reminder to test@test.com
	Then Reminder is send with message: Email with instructions was sent to test@test.com

Scenario: Remind Password with nonexisted user
	Given I open Login page
		And I click Remind Password button
	When I send reminder to user@email.net
	Then I see an error: No user was found

Scenario: Remind Password with invalid email
	Given I open Login page
		And I click Remind Password button
	When I send reminder to <email>
	Then I see an error: Invalid email

Examples: 
	| email          |
	|                |
	| invalid@email  |
	| @invalid.email |

