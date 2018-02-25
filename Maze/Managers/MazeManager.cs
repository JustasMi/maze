using Maze.Exceptions;
using Maze.MazeGenerators;
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
		private readonly IMazeGenerator mazeGenerator;
		private readonly MazeConfigurationValidator mazeConfigurationValidator;

		public MazeManager(
			IMazeRepository mazeRepository,
			IMazeGenerator mazeGenerator,
			MazeConfigurationValidator mazeConfigurationValidator)
		{
			this.mazeRepository = mazeRepository;
			this.mazeConfigurationValidator = mazeConfigurationValidator;
			this.mazeGenerator = mazeGenerator;
		}

		public async Task GenerateMaze(MazeConfiguration mazeConfiguration)
		{
			ValidationOutcome outcome = Validation<MazeConfiguration>
				.Candidate(mazeConfiguration)
				.Validate(mazeConfigurationValidator)
				.Outcome();

			if (outcome.IsInvalid())
			{
				throw new ManagerException("Validation failed: " + outcome.GetValidationMessage());
			}

			var result = mazeGenerator.Generate(mazeConfiguration);

			/*
			var testMaze = new Models.Maze()
			{
				Configuration = mazeConfiguration
			};
			*/

			await mazeRepository.Create(result);
		}

		public Task<Models.Maze[]> GetMazes()
		{
			return Task.FromResult(mazeRepository.GetAll().ToArray());
		}
	}
}