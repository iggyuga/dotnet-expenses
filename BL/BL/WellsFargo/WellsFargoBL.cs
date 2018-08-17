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


		public List<IEnumerable<WellsFargoDTO>> GetExpenses(List<IEnumerable<WellsFargoDTO>> files)
		{
			List<IEnumerable<WellsFargoDTO>> result = new List<IEnumerable<WellsFargoDTO>>();
			foreach (var file in files)
			{
				var singleResult = file
									.Where(d => d.Description.Contains(BaseExpenses.MARKETS, StringComparison.OrdinalIgnoreCase))
									.Select(d => new WellsFargoDTO
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

		public List<IEnumerable<WellsFargoDTO.ExpensesSummaryDTO>> GetExpensesSummaries(List<IEnumerable<WellsFargoDTO>> files)
		{
			List<IEnumerable<WellsFargoDTO.ExpensesSummaryDTO>> result = new List<IEnumerable<WellsFargoDTO.ExpensesSummaryDTO>>();
			foreach (var file in files)
			{
				var singleResult = file
									.Where(d => d.Description.Contains(BaseExpenses.MARKETS, StringComparison.OrdinalIgnoreCase))
									.GroupBy(d => new { Category = d.Category })
									.Select(d => new WellsFargoDTO.ExpensesSummaryDTO
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
