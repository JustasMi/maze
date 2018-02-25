using Maze.Exceptions;
using Maze.Models;
using Maze.Repositories;
using Maze.Validation;
using Maze.Validation.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Managers
{
	public class MazeManager : IMazeManager
	{
		private readonly IMazeRepository mazeRepository;
		private readonly MazeConfigurationValidator mazeConfigurationValidator;

		public MazeManager(
			IMazeRepository mazeRepository,
			MazeConfigurationValidator mazeConfigurationValidator)
		{
			this.mazeRepository = mazeRepository;
			this.mazeConfigurationValidator = mazeConfigurationValidator;
		}

		public Task GenerateMaze(MazeConfiguration mazeConfiguration)
		{
			ValidationOutcome outcome = Validation<MazeConfiguration>
				.Candidate(mazeConfiguration)
				.Validate(mazeConfigurationValidator)
				.Outcome();

			if (outcome.IsInvalid())
			{
				throw new ManagerException("Validation failed: " + outcome.GetValidationMessage());
			}

			throw new NotImplementedException();
		}
	}
}