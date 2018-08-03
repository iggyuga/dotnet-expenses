namespace BL.Models.Expenses.WellsFargo
{
	using CsvHelper.Configuration;
	using System;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.Text;

	public class ExpensesDTO : BaseExpenses
    {
		public ExpensesDTO() : base() { }

		public override float Amount { get => base.Amount; set => base.Amount = value; }

		public override string Description { get => base.Description; set => base.Description = value; }

		public override DateTime TransDate { get => base.TransDate; set => base.TransDate = value; }

		public override string Category { get => "Groceries"; }

		public class ExpensesSummaryDTO : ExpensesDTO
		{
			public ExpensesSummaryDTO() : base() { }

			public override string Category { get => "Groceries"; }

			public float Total { get; set; }
		}
	}

	public sealed class MapExpensesDTO : ClassMap<ExpensesDTO>
	{
		public MapExpensesDTO()
		{
			AutoMap();
			Map(m => m.TransDate).TypeConverter();
			Map(m => m.Category).Constant("Groceries");
			//TODO: figure out this typeconverter shit
		}
	}


}
