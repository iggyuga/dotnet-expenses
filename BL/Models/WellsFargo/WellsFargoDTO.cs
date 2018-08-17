namespace BL.Models.Expenses.WellsFargo
{
	using CsvHelper.Configuration;
	using System;
	using System.ComponentModel;
	using System.Collections.Generic;
	using System.Text;

	public class WellsFargoDTO : BaseExpenses
    {
		public WellsFargoDTO() : base() { }

		public override decimal Amount { get => base.Amount; set => base.Amount = value; }

		public override string Description { get => base.Description; set => base.Description = value; }

		public override DateTime TransDate { get => base.TransDate; set => base.TransDate = value; }

		public override string Category { get => "Groceries"; }

		public class WellsFargoSummaryDTO : WellsFargoDTO
		{
			public WellsFargoSummaryDTO() : base() { }

			public override string Category { get => "Groceries"; }

			public decimal Total { get; set; }
		}
	}

	////public sealed class MapExpensesDTO : ClassMap<ExpensesDTO>
	////{
	////	public MapExpensesDTO()
	////	{
	////		AutoMap();
	////		Map(m => m.TransDate).TypeConverter();
	////		Map(m => m.Category).Constant("Groceries");
	////		//TODO: figure out this typeconverter shit
	////	}
	////}


}
