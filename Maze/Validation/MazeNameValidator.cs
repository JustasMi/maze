using Maze.Models;
using Maze.Validation.Common;
using System;

namespace Maze.Validation
{
	public class MazeNameValidator : IValidator<MazeConfiguration>
	{
		public ValidationResults Validate(MazeConfiguration validationCandidate)
		{
			return validationCandidate.Name.Length <= 25 ?
				ValidationResult.Success<MazeNameValidator>() :
				ValidationResult.Failure<MazeNameValidator>($"Name length is {validationCandidate.Name.Length} while it must not exceed {5} characters");
		}
	}
}