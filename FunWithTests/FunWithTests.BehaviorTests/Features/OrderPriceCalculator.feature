Feature: Order price should be calculated with discount
	As a user
	I want to have a discount 
	when bying order

Scenario: Regular order should not have a discount
	Given order with price of 100 USD
	When  order is RegularOrder type
	Then  price should be 100 USD

Scenario: Expensive order should have 100 USD discount
	Given order with price of 200 EUR
	And   100 USD converts to 80 EUR
	When  order is ExpensiveOrder type
	Then  price should be 120 EUR
