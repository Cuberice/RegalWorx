using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegalWorxData
{
	public static class Extentions
	{
		public static string GetRandomString(int length)
		{
			string chars = Guid.NewGuid().ToString();
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
