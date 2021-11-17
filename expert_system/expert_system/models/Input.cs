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
		public List<string> lines { get; set; }
		public List<Rule> rules { get; set; }
		public List<Fact> facts { get; set; }
		public List<Query> queries { get; set; }

		public Input()
		{
			lines = new List<string>();
		}
		public bool ParseInput()
		{
			try
			{
				
				return true;
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
			var tokens = line.Split(" ");
			if (Common.IsValidRule(tokens))
			{
				var foobar = "";
			}
			return Enum_ParseState.FACT;
		}
		private Enum_ParseState ParseFacts(string line)
		{
			return Enum_ParseState.QUERY;
		}
		private Enum_ParseState ParseQueries(string line)
		{
			return Enum_ParseState.FINISH;
		}
		private bool ParseLines()
		{
			try
			{
				foreach (string line in File.ReadLines(input_file))
				{
					lines.Add(line);
					if (_state != Enum_ParseState.FINISH)
					{
						var initialized_line = line.Split("#")[0];
						initialized_line = line.Replace(@"/[^\S\r\n] /", " ");
						if (!string.IsNullOrEmpty(initialized_line))
						{
							_state = _stateFunctions[_state](line);
						}
					}
				}
			}
			catch (Exception e)
			{

			}
			return false;
		}
	}
}
