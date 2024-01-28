Feature: Login

As a user of sauce.demo store, I want to successfully login in order to perform purchase actions

@tag1
Scenario: Login to the sauce.demo store
	Given I am on the login page
	When I login using next credentials
		| username      | password     |
		| standard_user | secret_sauce |
	Then I see 'Products' list
