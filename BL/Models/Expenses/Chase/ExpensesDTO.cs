namespace BL.Models.Expenses.Chase
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class ExpensesDTO : BaseExpenses
	{
		public ExpensesDTO() : base() { }

		public string Type { get; set; }

		public override DateTime TransDate { get => base.TransDate; set => base.TransDate = value; }

		public DateTime PostDate { get; set; }

		public override string Description { get => base.Description; set => base.Description = value; }

		public override float Amount { get => base.Amount; set => base.Amount = value; }

		public override string Category { get => base.Category; set => base.Category = value; }

		public string Memo { get; set; }



		public class ExpensesSummaryDTO : ExpensesDTO
		{
			public ExpensesSummaryDTO() : base() { }

			public override string Category { get => base.Category; set => base.Category = value; }

			public float Total { get; set; }
		}
	}
}
