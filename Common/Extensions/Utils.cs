namespace Common.Extensions
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;

	public static class Utils
    {
		public static bool In<T>(this T input, params T[] values)
		{
			foreach (T v in values)
			{
				// need a null check before input.Equals()
				if ((input == null && v == null) || (input != null && input.Equals(v)))
				{
					return true;
				}
			}

			return false;
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source?.IndexOf(toCheck, comp) >= 0;
		}

		public static bool Contains(this string source, string[] toCheck, StringComparison comp)
		{
			foreach (var s in toCheck)
			{
				if (source?.IndexOf(s, comp) >= 0) { return true; }
			}
			return false;
		}
	}
}
