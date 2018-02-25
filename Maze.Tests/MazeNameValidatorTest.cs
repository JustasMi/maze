using Maze.Models;
using Maze.Validation;
using Maze.Validation.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Maze.Tests
{
	[TestClass]
	public class MazeNameValidatorTest
	{
		[TestMethod, Description("Test valid name")]
		public void TestMethod1()
		{
			MazeNameValidator mazeNameValidator = new MazeNameValidator();
			ValidationResults validationResults = mazeNameValidator.Validate(new MazeConfiguration() { Name = "hi" });
			Assert.IsTrue(validationResults.First().IsValid);
		}

		[TestMethod, Description("Test invalid name")]
		public void TestMethod2()
		{
			MazeNameValidator mazeNameValidator = new MazeNameValidator();
			ValidationResults validationResults = mazeNameValidator.Validate(new MazeConfiguration() { Name = "hiasdsadsadsadsadsadsadsadsadsadsadsadsadsadsadsadsadas" });
			Assert.IsFalse(validationResults.First().IsValid);
		}
	}
}