namespace ExpensesApp
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.IO;
	using System.Linq;
	using Common.Extensions;
	using BL.BL;
	using BL.Models.Expenses;
	using System.Threading;

	public class Program
	{
		// First we want to locate the directory where the files are kept, we can either get it from input or use a hardcoded version
		static string BASE_PATH = @"C:\Users\ignacio.rosas\Documents\banking";

		const string CHASE = "Chase";
		const string WELLSFARGO = "WellsFargo";

		private static void _Help()
		{
			Console.WriteLine("Console arguments: -p \"<pathname>\"\n To use default pathing use: -base");
		}

		private static void StartMenu(string[] args)
		{
			while (!args[0].StartsWith("-") || args[0].ToLower() == "h" || args[0].ToLower() == "help")
			{
				_Help();
				string[] input = Console.ReadLine().Split(' ');
				if (args.Length > 2 || args.Length < 1) { StartMenu(args); }
				for (int i = 0; i < input.Length; i++)
				{
					args[i] = input[i];
				}
			}
		}

		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the Expense calulator");
			Thread.Sleep(1000);

			StartMenu(args);

			try
			{
				switch(args[0].ToLower())
				{
					case "-p":
						Run(args, BASE_PATH);
						break;
					case "-base":
						Run(args, BASE_PATH);
						break;
					default:
						_Help();
						break;
				}
			}
			catch(Exception e) { throw e;  }

		}

		private static void Run(string[] args, string bASE_PATH)
		{
			SetPath(args);
			NameValueCollection files = FileWorker(bASE_PATH);
			var chaseFiles = FileProcessor.RetrieveFilesPerBank(files, CHASE);
			var wellsFiles = FileProcessor.RetrieveFilesPerBank(files, WELLSFARGO);

			List<IEnumerable<BL.Models.Expenses.Chase.ExpensesDTO>> chase = (List<IEnumerable<BL.Models.Expenses.Chase.ExpensesDTO>>)BaseExpenses.ReadInCSV<IEnumerable<BL.Models.Expenses.Chase.ExpensesDTO>>(chaseFiles, CHASE);
			List<IEnumerable<BL.Models.Expenses.WellsFargo.ExpensesDTO>> wells = (List<IEnumerable<BL.Models.Expenses.WellsFargo.ExpensesDTO>>)BaseExpenses.ReadInCSV<IEnumerable<BL.Models.Expenses.WellsFargo.ExpensesDTO>>(wellsFiles, WELLSFARGO);

			ChaseBL chaseBL = new ChaseBL();
			var mainChaseReport = chaseBL.GetExpenses(chase);
			var summaryChaseReport = chaseBL.GetExpensesSummaries(chase);

			WellsFargoBL wellsFargoBL = new WellsFargoBL();
			var mainWellsReport = wellsFargoBL.GetExpenses(wells);
			var summaryWellsReport = wellsFargoBL.GetExpensesSummaries(wells);

			BaseExpenses.PrintExpenses(mainChaseReport);
			BaseExpenses.PrintExpenses(summaryChaseReport);

			BaseExpenses.PrintExpenses(mainWellsReport);
			BaseExpenses.PrintExpenses(summaryWellsReport);

		}

		private static NameValueCollection FileWorker(string BASE_PATH)
		{
			return FileProcessor.ProcessDirectory(BASE_PATH);
		}

		private static void SetPath(string[] args = null)
		{
			if (args.Length == 1) { return; }
			else if (args.Length == 2) { BASE_PATH = args[1]; }
			else { throw new Exception("You entered an invalid number of arguments"); }
		}

	}
}
