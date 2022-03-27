Feature: Available order types

Scenario: Order type has correct discount
	Given Order types should have correct discount
		| Name | Discount | Currency |
		| RegularOrder | 0 | USD |
		| ExpensiveOrder | 100 | USD |