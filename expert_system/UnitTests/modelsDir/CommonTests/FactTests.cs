using Microsoft.VisualStudio.TestTools.UnitTesting;
using expert_system.models;
using System;

namespace UnitTests.modelsDir.CommonTests
{
	[TestClass]
	public class FactTests
	{
		[TestMethod]
		[DataRow("=ABC")]
		[DataRow("=ABC#valid comment")]
		[DataRow("=")]
		[DataRow("= A B C D E F G H")]
		public void IsValidRule_Tests(string line)
		{
			Assert.IsTrue(Common.IsValidFact(line));
		}

		[TestMethod]
		[DataRow("ABC")]
		[DataRow("=>ABC")]
		[DataRow("=AbC")]
		[DataRow("=a")]
		[DataRow("a")]
		[DataRow("#= A B C D E F G H")]
		public void IsNotValidRule_Tests(string line)
		{
			Assert.IsFalse(Common.IsValidFact(line));
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
