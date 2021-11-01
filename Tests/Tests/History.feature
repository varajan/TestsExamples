Feature: History

Background: 
	Given Setting have default values
		And History is cleared

Scenario: Clear history
	Given I have history data:
		| Amount | %  | Term |
		| 1000   | 10 | 100  |
		| 1000   | 20 | 50   |
	When I open History page
		And I clear the history
	Then the History is:
		| Amount | % | Term | Year | From | To | Interest | Income |

Scenario: History table
	Given I have history data:
		| Amount | %  | Term | Fin Year | Start Date |
		| 1000   | 10 | 100  | 360      | 2022/01/10 |
		| 1000   | 20 | 50   | 365      | 2022/05/15 |
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From       | To         | Interest | Income   |
		| 1,000.00 | 20% | 50   | 365  | 15/05/2022 | 04/07/2022 | 27.40    | 1,027.40 |
		| 1,000.00 | 10% | 100  | 360  | 10/01/2022 | 20/04/2022 | 27.78    | 1,027.78 |

Scenario: History shows last 9 rows
	Given I have history data:
		| Amount | %  | Term | Fin Year | Start Date |
		| 1001   | 1  | 101  | 360      | 2022/01/01 |
		| 1002   | 2  | 102  | 365      | 2022/05/02 |
		| 1003   | 3  | 103  | 360      | 2022/01/03 |
		| 1004   | 4  | 104  | 365      | 2022/05/04 |
		| 1005   | 5  | 105  | 360      | 2022/01/05 |
		| 1006   | 6  | 106  | 365      | 2022/05/06 |
		| 1007   | 7  | 107  | 360      | 2022/01/07 |
		| 1008   | 8  | 108  | 365      | 2022/05/08 |
		| 1009   | 9  | 109  | 360      | 2022/01/09 |
		| 1010   | 10 | 110  | 365      | 2022/05/10 |
		| 1011   | 11 | 111  | 360      | 2022/01/11 |
		| 1012   | 12 | 112  | 365      | 2022/05/12 |
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From       | To         | Interest | Income   |
		| 1,012.00 | 12% | 112  | 365  | 12/05/2022 | 01/09/2022 | 37.26    | 1,049.26 |
		| 1,011.00 | 11% | 111  | 360  | 11/01/2022 | 02/05/2022 | 34.29    | 1,045.29 |
		| 1,010.00 | 10% | 110  | 365  | 10/05/2022 | 28/08/2022 | 30.44    | 1,040.44 |
		| 1,009.00 | 9%  | 109  | 360  | 09/01/2022 | 28/04/2022 | 27.50    | 1,036.50 |
		| 1,008.00 | 8%  | 108  | 365  | 08/05/2022 | 24/08/2022 | 23.86    | 1,031.86 |
		| 1,007.00 | 7%  | 107  | 360  | 07/01/2022 | 24/04/2022 | 20.95    | 1,027.95 |
		| 1,006.00 | 6%  | 106  | 365  | 06/05/2022 | 20/08/2022 | 17.53    | 1,023.53 |
		| 1,005.00 | 5%  | 105  | 360  | 05/01/2022 | 20/04/2022 | 14.66    | 1,019.66 |
		| 1,004.00 | 4%  | 104  | 365  | 04/05/2022 | 16/08/2022 | 11.44    | 1,015.44 |

Scenario: History respect settings
	Given I update settings: '<number format>', '<date format>', '<currency>'
		And I have history data:
		| Amount | %  | Term | Fin Year | Start Date |
		| 100000 | 99 | 299  | 360      | 2022/01/10 |
	When I open History page
	Then the History is:
		| Amount   | %   | Term | Year | From   | To   | Interest   | Income   |
		| <amount> | 99% | 299  | 360  | <from> | <to> | <interest> | <income> |

	Examples: 
	| currency                | number format  | date format | from       | to         | amount     | interest  | income     |
	| $ - US dollar           | 123.456.789,00 | dd-MM-yyyy  | 10-01-2022 | 05-11-2022 | 100.000,00 | 82.225,00 | 182.225,00 |
	| € - euro                | 123 456 789.00 | MM/dd/yyyy  | 01/10/2022 | 11/05/2022 | 100 000.00 | 82 225.00 | 182 225.00 |
	| £ - Great Britain Pound | 123 456 789,00 | MM dd yyyy  | 01 10 2022 | 11 05 2022 | 100 000,00 | 82 225,00 | 182 225,00 |

