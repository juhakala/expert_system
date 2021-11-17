using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system.models
{
	public enum Enum_ParseState
	{
		RULE,
		FACT,
		QUERY,
		FINISH
	}
	public enum Enum_FormulaMembers
	{
		OPERAND,
		OPERATOR,
		EQUALITY
	}
	public enum Enum_Operator
	{
		AND,
		OR,
		XOR,
	}
	public enum Enum_Equality
	{
		IMPLIES,
		IFANDONLYIF
	}

	static class Common
	{
		static public Dictionary<Enum_Operator, string> Dict_FormulaMembers = new Dictionary<Enum_Operator, string>()
		{
			{ Enum_Operator.AND, @"+" },
			{ Enum_Operator.OR, @"|" },
			{ Enum_Operator.XOR, @"^" },
		};

		static public Dictionary<Enum_Equality, string> Dict_Equality = new Dictionary<Enum_Equality, string>()
		{
			{ Enum_Equality.IMPLIES, @"=>" },
			{ Enum_Equality.IFANDONLYIF, @"<=>" },
		};

		static public bool CheckIfFileOk(string filename)
		{
			if (File.Exists(filename))
			{
				return true;
			}
			return false;
		}
		static private bool IsValidOperand(string item)
		{
			if (item.Length == 1 && item[0] >= 'A' && item[0] >= 'Z')
			{
				return true;
			}
			return false;
		}
		static private bool IsValidOperator(string item)
		{
			if (item.Length == 1 && Dict_FormulaMembers.ContainsValue(item))
			{
				return true;
			}
			return false;
		}
		static private bool IsValidEquality(string item)
		{
			if (item.Length == 1 && Dict_Equality.ContainsValue(item))
			{
				return true;
			}
			return false;
		}
		static public bool IsValidRule(string[] tokens)
		{
			bool state = false;
			bool equality = false;
			for (int i = 0; i < tokens.Length; i++)
			{
				if (state == false && IsValidOperand(tokens[i]))
					state = true;
				else if (state == true && IsValidOperator(tokens[i]))
					state = false;
				else if (state == false && IsValidEquality(tokens[i]))
					equality = true;
			}
			return state && equality;
		}
		static public bool IsValidRule(string line)
		{
			return (IsValidRule(line.Split(" ")));
		}
	}
}
