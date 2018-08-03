namespace BL.Models.Expenses.Chase
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class ExpensesDTO : BaseExpenses
	{
		public ExpensesDTO() : base() { }

		public DateTime PostDate { get; set; }

		public class ExpensesSummaryDTO : ExpensesDTO
		{
			public ExpensesSummaryDTO() { }

			public float Total { get; set; }
		}
	}
}
