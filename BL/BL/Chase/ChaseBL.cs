namespace BL.BL
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Common.Extensions;
	using Models.Expenses.Chase;
	using Models.Expenses;

	public class ChaseBL
    {
		public ChaseBL() { }

		public List<IEnumerable<ExpensesDTO>> GetExpenses(List<IEnumerable<ExpensesDTO>> files)
		{
			List<IEnumerable<ExpensesDTO>> result = new List<IEnumerable<ExpensesDTO>>();
			foreach (var file in files)
			{
				var singleResult = file
									.Where(d => d.Category.In(BaseExpenses.CATEGORIES))
									.Select(d => new ExpensesDTO
									{
										Category = d.Category,
										Amount = d.Amount,
										Description = d.Description,
										TransDate = d.TransDate,
										PostDate = d.PostDate,
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
									.Where(d => d.Category.In(BaseExpenses.CATEGORIES))
									.GroupBy(d => new { d.Category })
								.Select(d => new ExpensesDTO.ExpensesSummaryDTO
								{
									Category = d.Key.Category,
									Total = d.Sum(g => g.Amount),
								})
								.ToList();
				result.Add(singleResult);
			}

			return result;
		}






    }
}
