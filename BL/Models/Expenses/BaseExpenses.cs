namespace BL.Models.Expenses
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using CsvHelper;

	public abstract class BaseExpenses
	{
		public static readonly string[] MARKETS = { "Earth Fare", "ALDI", "Fresh Market", "Fresh Mkt", "Wal-Mart", "WHOLEFDS", "Whole Foods", };

		public static readonly string[] CATEGORIES = { "Dining Out", "Groceries" };

		public BaseExpenses() { }

		public virtual string Category { get; set; }

		public DateTime TransDate { get; set; }

		public string Description { get; set; }

		public float Amount { get; set; }

		public static void PrintExpenses<T>(IList<IEnumerable<T>> data)
		{

			foreach (var d in data)
			{
				Console.WriteLine(d);
			}
		}

		public static IList<T> ReadInCSV<T>(string[] files, string bank)
		{
			if (string.IsNullOrEmpty(bank) || files == null)
			{
				return null;
			}
			if (bank.ToLower() == "chase")
			{
				IList<IEnumerable<Chase.ExpensesDTO>> allValues = null;
				foreach (var file in files)
				{
					using (TextReader fileReader = File.OpenText(file))
					{
						var csv = new CsvReader(fileReader);
						csv.Configuration.HasHeaderRecord = true;
						csv.Configuration.IgnoreQuotes = true;
						var record = csv.GetRecords<Chase.ExpensesDTO>();
						allValues.Add(record);
					}
				}

				return (IList<T>)(object)allValues;
			}
			else
			{
				IList<IEnumerable<WellsFargo.ExpensesDTO>> allValues = null;
				foreach (var file in files)
				{
					using (TextReader fileReader = File.OpenText(file))
					{
						var csv = new CsvReader(fileReader);
						csv.Configuration.HasHeaderRecord = false;
						csv.Configuration.IgnoreQuotes = true;
						allValues.Add(csv.GetRecords<WellsFargo.ExpensesDTO>());
					}
				}
				return (IList<T>)(object)allValues;
			}

		}
	}
}
