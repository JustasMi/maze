using Maze.MazeGenerators;
using Maze.Models;
using Maze.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Managers
{
	public class MazeManager : IMazeManager
	{
		private readonly IMazeRepository mazeRepository;
		private readonly IMazeGenerator mazeGenerator;

		public MazeManager(
			IMazeRepository mazeRepository,
			IMazeGenerator mazeGenerator)
		{
			this.mazeRepository = mazeRepository;
			this.mazeGenerator = mazeGenerator;
		}

		public async Task GenerateMaze(MazeConfiguration mazeConfiguration)
		{
			Models.Maze maze = mazeGenerator.Generate(mazeConfiguration);
			await mazeRepository.Create(maze);
		}

		public Task<Models.Maze> Get(int id)
		{
			return mazeRepository.Get(id);
		}

		public Task<Models.Maze[]> GetAll()
		{
			return Task.FromResult(mazeRepository.GetAll().ToArray());
		}
	}
}