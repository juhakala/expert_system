using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system.models
{
	class Input
	{
		public string input_file { get; set; }
		private List<string> lines { get; set; }
		private List<Rule> rules { get; set; }
		private List<Fact> facts { get; set; }
		private List<Query> queries { get; set; }

		public Input()
		{
			lines = new List<string>();
			InitStateMachine();
		}
		public bool ParseInput()
		{
			try
			{
				while (string.IsNullOrEmpty(input_file) || string.IsNullOrWhiteSpace(input_file))
					input_file = Console.ReadLine();
				return ParseLines();
			}
			catch (Exception e)
			{
				Console.WriteLine($"ParseInput Exception: '{e}'");
				return false;
			}
		}
		private Dictionary<Enum_ParseState, Func<string, Enum_ParseState>> _stateFunctions = null;
		private Enum_ParseState _state;

		private void InitStateMachine()
		{
			_stateFunctions = new Dictionary<Enum_ParseState, Func<string, Enum_ParseState>>();

			_stateFunctions.Add(Enum_ParseState.RULE, ParseRules);
			_stateFunctions.Add(Enum_ParseState.FACT, ParseFacts);
			_stateFunctions.Add(Enum_ParseState.QUERY, ParseQueries);
			_state = Enum_ParseState.RULE;
		}
		private Enum_ParseState ParseRules(string line)
		{
			if (Common.IsValidRule(line))
			{
				return Enum_ParseState.RULE;
			}
			return Enum_ParseState.FACT;
		}
		private Enum_ParseState ParseFacts(string line)
		{
			if (Common.IsValidFact(line))
			{
				return Enum_ParseState.QUERY;
			}
			return Enum_ParseState.FINISH;
		}
		private Enum_ParseState ParseQueries(string line)
		{
			return Enum_ParseState.FINISH;
		}
		private bool ParseLines()
		{

			foreach (string line in File.ReadLines(input_file))
			{
				lines.Add(line);
				if (_state != Enum_ParseState.FINISH)
				{
					try
					{
						if (!string.IsNullOrEmpty(line))
						{
							_state = _stateFunctions[_state](line);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine($"_stateFunctions Exception: state: '{_state}', '{e}'");
						throw;
					}
				}
			}
			if (_state != Enum_ParseState.FINISH)
				return false;
			else
				return true;
		}
	}
}
