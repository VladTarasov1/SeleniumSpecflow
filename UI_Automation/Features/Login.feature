Feature: Login

As a user of sauce.demo store, I want to successfully login in order to perform purchase actions

@pass
Scenario: Login
	Given I am on the login page
	When I login using next credentials
		| username      | password     |
		| standard_user | secret_sauce |
	Then I see 'Products' list

#remove ignore if you want to trigger screenshots on failure
@ignore
@fail
Scenario: Login failed - provoke screenshot in report
	Given I am on the login page
	When I login using next credentials
		| username      | password      |
		| wrongUsername | wrongPassword |
	Then I see 'Products' list