namespace Common.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.IO;
	using CsvHelper;

	public static class FileProcessor
    {
		public static NameValueCollection ProcessDirectory(string path)
		{
			NameValueCollection processedFiles = null;

			// If path passed in is a directory, pull 
			// all the files from the directory to be parsed
			if (Directory.Exists(path))
			{
				try
				{
					processedFiles = ProcessFiles(Directory.GetFiles(path));
				} catch (Exception e)
				{
					Console.WriteLine("{0} Exception caught.", e);
				}
			}
			// Must be a single file
			else if (File.Exists(path))
			{
				try
				{
					processedFiles = ProcessFiles(new string[] { path });
				}
				catch (Exception e)
				{
					Console.WriteLine("{0} Exception caught.", e);
				}
			}
			return processedFiles;
		}

		public static NameValueCollection ProcessFiles(string[] files)
		{
			NameValueCollection nvc = new NameValueCollection();
			foreach (var file in files)
			{
				if (file.Contains("Chase", StringComparison.OrdinalIgnoreCase))
				{
					nvc.Add("Chase", file);
				}
				else
				{
					nvc.Add("WellsFargo", file);
				}
			}
			return nvc;
		}

		public static string[] RetrieveFilesPerBank(NameValueCollection files, string bank)
		{
			if (!string.IsNullOrEmpty(bank) && files[bank] != null)
			{
				return files[bank].Split(',', StringSplitOptions.RemoveEmptyEntries);
			}

			return null;		
		}
		
	}
}
