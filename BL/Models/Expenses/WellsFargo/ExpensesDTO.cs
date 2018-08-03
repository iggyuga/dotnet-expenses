namespace BL.Models.Expenses.WellsFargo
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class ExpensesDTO : BaseExpenses
    {
		public ExpensesDTO() { }

		public override string Category { get => "Groceries"; }

		public class ExpensesSummaryDTO : ExpensesDTO
		{
			public float Total { get; set; }
		}
	}
}
