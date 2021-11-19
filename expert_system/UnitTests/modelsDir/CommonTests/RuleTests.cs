using Microsoft.VisualStudio.TestTools.UnitTesting;
using expert_system.models;
using System;

namespace UnitTests.modelsDir.CommonTests
{
	[TestClass]
	public class RuleTests
	{
		[TestMethod]
		[DataRow("A + B => C")]
		[DataRow("A + B => B")]
		[DataRow("A + B => B #hyväksytty kommentti")]
		[DataRow("A + B => B | D # #hyväksytty kommentti")]
		public void IsValidRule_Tests(string line)
		{
			Assert.IsTrue(Common.IsValidRule(line));
		}

		[TestMethod]
		[DataRow("A + B = C")]
		[DataRow("A + B => c")]
		[DataRow("A + BB => B")]
		[DataRow("A + B A => B #hyväksytty kommentti")]
		[DataRow("A + + B => B | D # #hyväksytty kommentti")]
		[DataRow("A + + B > B | D # #hyväksytty kommentti")]
		[DataRow("A + B + B | D # #hyväksytty kommentti")]
		[DataRow("A + => D # #hyväksytty kommentti")]
		[DataRow("A + B => D hylätty kommentti")]
		[DataRow("A #+ B => D hylätty kommentti")]
		[DataRow("#A + B => D hylätty kommentti")]
		public void IsNotValidRule_Tests(string line)
		{
			Assert.IsFalse(Common.IsValidRule(line));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[DataRow("")]
		[DataRow(null)]
		public void IsNotValidRuleNullOrEmpty_Tests(string line)
		{
			Common.IsValidRule(line);
		}
	}
}
