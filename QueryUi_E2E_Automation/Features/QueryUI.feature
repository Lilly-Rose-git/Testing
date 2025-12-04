@ui
Feature: Opening QueryUI

Scenario Outline: Open and search on QueryUI page
	Given I am using Query UI with account type uk-ops
	Then I am on the home-page page
	When I click the "costs" menu item
	And I click the "query-costs" submenu item
	Then I am on the query-costs page
	When I add the search pill: SCR Number | Equals | 50308296
	And I submit the search


Scenario Outline: Open, search, select record and Clone on QueryUI page
	Given I am using Query UI with account type uk-ops
	Then I am on the home-page page
	When I click the "trades" menu item
	And I click the "query-swaps" submenu item
	Then I am on the query-swaps page
	When I add the search pill in search bar: Trade Number | Equals | 54134731
	And I submit the search
	When I select row 0 in checkbox column in the swaps list search results grid
	And I right click the checkbox column on the 1st row in the swaps list search results grid
	Then I click Copy Trade context menu item on swaps list search results grid
	Then I click the "Quantity" content in the "1" row of the "swaps list search results" grid
	When I set the value in the "Quantity" column in the "1" row to be "100" within the "swaps list search results" grid
	When I set the value in the "Fixed Price" column in the "1" row to be "99" within the "swaps list search results" grid
	And I click the "save" button
	When I Wait for 60 seconds
	#72322117
	#53733005













#    | TradeType |   TraderInitials | IsBrokered | StrategyNum | OperatingDesk | CompanyCode |
#	 | External  |   NonUS4         | false      | NonUS4      | NonUS4        | NonUS4      |
#	Given I am using PhysOps with account type <Role>
#	And I go to the cashflows page and the watch-list tab
#	When I add the search pill: Trade Number | Equals 
#	And I submit the search
#	When I select row 0 in checkbox column
#	Then The <Field> should be in expected <State>
	

#	Examples:
#	| Role   | Field             | State    |
#	| ukops	 | convert pp button | unlocked |