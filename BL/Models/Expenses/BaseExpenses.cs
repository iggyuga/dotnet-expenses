namespace BL.Models.Expenses
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using CsvHelper;

	public abstract class BaseExpenses
	{
		public static readonly string[] MARKETS = { "Earth Fare", "ALDI", "Fresh Market", "Fresh Mkt", "Wal-Mart", "WHOLEFDS", "Whole Foods", };

		public static readonly string[] CATEGORIES = { "Dining Out", "Groceries" };

		public BaseExpenses() { }

		public virtual string Category { get; set; }

		public virtual DateTime TransDate { get; set; }

		public virtual string Description { get; set; }

		public virtual float Amount { get; set; }

		public static void PrintExpenses<T>(List<IEnumerable<T>> data)
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
				List<IEnumerable<Chase.ExpensesDTO>> allValues = new List<IEnumerable<Chase.ExpensesDTO>>();
				foreach (var file in files)
				{
					using (TextReader fileReader = File.OpenText(file))
					{
						var csv = new CsvReader(fileReader);
						csv.Configuration.HasHeaderRecord = true;
						csv.Configuration.PrepareHeaderForMatch = header => Regex.Replace(header, @"\s", string.Empty);
						var record = csv.GetRecords<Chase.ExpensesDTO>().ToList();
						allValues.Add(record);
					}
				}

				return (IList<T>)allValues;
			}
			else
			{
				List<IEnumerable<WellsFargo.ExpensesDTO>> allValues = new List<IEnumerable<WellsFargo.ExpensesDTO>>();
				foreach (var file in files)
				{
					using (TextReader fileReader = File.OpenText(file))
					{
						var csv = new CsvReader(fileReader);
						csv.Configuration.HasHeaderRecord = false;
						csv.Configuration.PrepareHeaderForMatch = header => Regex.Replace(header, @"\s", string.Empty);
						var record = csv.GetRecords<WellsFargo.ExpensesDTO>().ToList();
						allValues.Add(record);
					}
				}
				return (IList<T>)allValues;
			}

		}
	}
}
