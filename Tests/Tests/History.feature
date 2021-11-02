Feature: History

Scenario: Clear history
	Given I login as 'Jack'
		And I have history data:
		| Amount | %  | Term | Interest | Income |
		| 1000   | 10 | 100  | 50       | 1050   |
		| 1000   | 20 | 50   | 100      | 1100   |
	When I open History page
		And I clear the history
	Then the History is:
		| Amount | % | Term | Year | From | To | Interest | Income |

Scenario: History table
	Given I login as 'Jack'
		And I have history data:
		| Amount | %  | Term | Fin Year | Start Date | Interest | Income |
		| 1000   | 10 | 100  | 360      | 10/01/2022 | 50       | 1050   |
		| 1000   | 20 | 50   | 365      | 15/05/2022 | 100      | 1100   |
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From       | To         | Interest | Income   |
		| 1,000.00 | 20% | 50   | 365  | 15/05/2022 | 04/07/2022 | 100.00   | 1,100.00 |
		| 1,000.00 | 10% | 100  | 360  | 10/01/2022 | 20/04/2022 | 50.00    | 1,050.00 |

Scenario: History shows last 9 rows
	Given I login as 'Jack'
		And I have history data:
		| Amount | %  | Term | Fin Year | Start Date | Income | Interest |
		| 1001   | 1  | 101  | 360      | 01/01/2022 | 10010  | 10       |
		| 1002   | 2  | 102  | 365      | 02/05/2022 | 10020  | 20       |
		| 1003   | 3  | 103  | 360      | 03/01/2022 | 10030  | 30       |
		| 1004   | 4  | 104  | 365      | 04/05/2022 | 10040  | 40       |
		| 1005   | 5  | 105  | 360      | 05/01/2022 | 10050  | 50       |
		| 1006   | 6  | 106  | 365      | 06/05/2022 | 10060  | 60       |
		| 1007   | 7  | 107  | 360      | 07/01/2022 | 10070  | 70       |
		| 1008   | 8  | 108  | 365      | 08/05/2022 | 10080  | 80       |
		| 1009   | 9  | 109  | 360      | 09/01/2022 | 10090  | 90       |
		| 1010   | 10 | 110  | 365      | 10/05/2022 | 10100  | 100      |
		| 1011   | 11 | 111  | 360      | 11/01/2022 | 10110  | 110      |
		| 1012   | 12 | 112  | 365      | 12/05/2022 | 10120  | 120      |
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From       | To         | Interest | Income    |
		| 1,012.00 | 12% | 112  | 365  | 12/05/2022 | 01/09/2022 | 120.00   | 10,120.00 |
		| 1,011.00 | 11% | 111  | 360  | 11/01/2022 | 02/05/2022 | 110.00   | 10,110.00 |
		| 1,010.00 | 10% | 110  | 365  | 10/05/2022 | 28/08/2022 | 100.00   | 10,100.00 |
		| 1,009.00 | 9%  | 109  | 360  | 09/01/2022 | 28/04/2022 | 90.00    | 10,090.00 |
		| 1,008.00 | 8%  | 108  | 365  | 08/05/2022 | 24/08/2022 | 80.00    | 10,080.00 |
		| 1,007.00 | 7%  | 107  | 360  | 07/01/2022 | 24/04/2022 | 70.00    | 10,070.00 |
		| 1,006.00 | 6%  | 106  | 365  | 06/05/2022 | 20/08/2022 | 60.00    | 10,060.00 |
		| 1,005.00 | 5%  | 105  | 360  | 05/01/2022 | 20/04/2022 | 50.00    | 10,050.00 |
		| 1,004.00 | 4%  | 104  | 365  | 04/05/2022 | 16/08/2022 | 40.00    | 10,040.00 |

Scenario: History respect settings
	Given I login as 'Jack'
		And I update settings: '<number format>', '<date format>', '<currency>'
		And I open Deposit page
		And I select Start Date as '10/11/2022'
		And I calculate deposit for $100000 with 99% on 299 days
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From   | To   | Interest   | Income   |
		| <amount> | 99% | 299  | 365  | <from> | <to> | <interest> | <income> |

	Examples: 
	| currency                | number format  | date format | from       | to         | amount     | interest  | income     |
	| $ - US dollar           | 123.456.789,00 | dd-MM-yyyy  | 11-10-2022 | 06-08-2023 | 100.000,00 | 81.098,63 | 181.098,63 |
	| € - euro                | 123 456 789.00 | MM/dd/yyyy  | 10/11/2022 | 08/06/2023 | 100 000.00 | 81 098.63 | 181 098.63 |
	| £ - Great Britain Pound | 123 456 789,00 | MM dd yyyy  | 10 11 2022 | 08 06 2023 | 100 000,00 | 81 098,63 | 181 098,63 |

