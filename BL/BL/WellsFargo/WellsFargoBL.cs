namespace BL.BL
{
	using Common.Extensions;
	using Models.Expenses.WellsFargo;
	using Models.Expenses;
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Linq;


	public class WellsFargoBL
    {
		public WellsFargoBL() { }


		public List<IEnumerable<ExpensesDTO>> GetExpenses(List<IEnumerable<ExpensesDTO>> files)
		{
			List<IEnumerable<ExpensesDTO>> result = new List<IEnumerable<ExpensesDTO>>();
			foreach (var file in files)
			{
				var singleResult = file
									.Where(d => d.Description.Contains(BaseExpenses.MARKETS, StringComparison.OrdinalIgnoreCase))
									.Select(d => new ExpensesDTO
									{
										Amount = d.Amount,
										Category = d.Category,
										Description = d.Description,
										TransDate = d.TransDate,
									})
									.ToList();
				result.Add(singleResult);
			}

			return result;
		}

		public List<IEnumerable<ExpensesDTO.ExpensesSummaryDTO>> GetExpensesSummaries(List<IEnumerable<ExpensesDTO>> files)
		{
			List<IEnumerable<ExpensesDTO.ExpensesSummaryDTO>> result = new List<IEnumerable<ExpensesDTO.ExpensesSummaryDTO>>();
			foreach (var file in files)
			{
				var singleResult = file
									.Where(d => d.Description.Contains(BaseExpenses.MARKETS, StringComparison.OrdinalIgnoreCase))
									.GroupBy(d => new { Category = d.Category })
									.Select(d => new ExpensesDTO.ExpensesSummaryDTO
									{
										Category = d.Key.Category,
										Total = d.Sum(g => g.Amount)
									})
									.ToList();
				result.Add(singleResult);

			}

			return result;
		}
    }
}
