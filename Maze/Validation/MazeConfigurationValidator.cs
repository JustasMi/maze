using Maze.Models;
using Maze.Validation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Validation
{
	public class MazeConfigurationValidator : IValidator<MazeConfiguration>
	{
		private readonly MazeNameValidator mazeNameValidator;

		public MazeConfigurationValidator(MazeNameValidator mazeNameValidator)
		{
			this.mazeNameValidator = mazeNameValidator;
		}

		public ValidationResults Validate(MazeConfiguration validationCandidate)
		{
			ValidationOutcome outcome = Validation<MazeConfiguration>
				.Candidate(validationCandidate)
				.Validate(mazeNameValidator)
				.Outcome();

			return outcome.Results;
		}
	}
}