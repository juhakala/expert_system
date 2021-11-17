using expert_system.models;
using System;

namespace expert_system
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Controller cont = new Controller();
			if (args.Length > 0)
			{
				if (Common.CheckIfFileOk(args[0]))
				{
					cont.input.input_file = args[0];
				}
			}
		}
	}
}
