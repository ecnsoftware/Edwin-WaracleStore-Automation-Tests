Feature: Release 0.1
release 0.1 of the Waracle Store Automation Tests includes the following scenarios:
AC-1    A customer can apply a coupon code from the cart.
AC-2    Applying WARACLE25 reduces the subtotal by 25%.
AC-3     Standard shipping of £5.00 applies to any non-empty basket. 
AC-4     Order total = subtotal − discount + shipping.
AC-5    An invalid or empty code applies no discount and shows a clear message.
AC-6    The order conﬁrmation shows the applied coupon, the discount and the ﬁnal total.

Background: 
	Given I navigate to Waracle Store
	When I login to the account
	Then I should be on the home page

@AC-1
Scenario: A customer can apply a coupon code from the cart
	When I add an item to the cart
		| Gender | ItemName           | Quantity |
		| Men    | Men's Grey T-Shirt |        2 |	
	And I apply the coupon code "WARACLE25"
	Then the discount should be applied with the message "Coupon “WARACLE25” applied"

@AC-2
Scenario: Applying WARACLE25 reduces the subtotal by 25%
	When I add an item to the cart
		| Gender | ItemName           | Quantity |
		| Men    | Men's Grey T-Shirt |        2 |
	And I apply the coupon code "WARACLE25"
	Then the correct discount amount should be displayed as "25%"

@AC-3	
Scenario: Standard shipping of £5.00 applies to any non-empty basket
	When I add an item to the cart
		| Gender | ItemName           | Quantity |
		| Men    | Men's Grey T-Shirt |        2 |
	Then the shipping cost should be displayed as "£5.00"

@AC-4
Scenario: Order total = subtotal − discount + shipping
	When I add an item to the cart
		| Gender | ItemName           | Quantity |
		| Men    | Men's Grey T-Shirt |        2 |
	And I apply the coupon code "WARACLE25"
	Then the order total should be calculated correctly "25%"

@AC-5
Scenario: An invalid or empty code applies no discount and shows a clear message
	When I add an item to the cart
		| Gender | ItemName           | Quantity |
		| Men    | Men's Grey T-Shirt |        1 |
	And I apply the coupon code "INVALIDCODE"
	Then the discount should not be applied with the message "Invalid coupon code"