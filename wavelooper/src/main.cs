using System;
using System.IO;
using System.Reflection;
using ambient.wavelooper;
using NUnit.Framework;
using NUnit.ConsoleRunner;

namespace ambient.wavelooper
{
	public class Program
	{
		static void Main(string[] args)
		{
			//string[] my_args = { Assembly.GetExecutingAssembly().Location };
			//string[] tests = { "bin/test_wave.dll" };
			int returnCode = NUnit.ConsoleRunner.Runner.Main(args);
			//Console.WriteLine(returnCode.ToString());
			if (returnCode != 0)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("Build is broken");
				Console.WriteLine();
				Console.WriteLine();
			}
			else
			{
				Console.BackgroundColor = ConsoleColor.Green;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("All Tests are GREEN");
				Console.WriteLine();
                                Console.WriteLine();

			}
		}
	}
}
