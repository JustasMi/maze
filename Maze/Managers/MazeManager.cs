using Maze.Factories;
using Maze.Models;
using Maze.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Managers
{
	public class MazeManager : IMazeManager
	{
		private readonly IMazeRepository mazeRepository;
		private readonly IMazeFactory mazeFactory;

		public MazeManager(
			IMazeRepository mazeRepository,
			IMazeFactory mazeFactory)
		{
			this.mazeRepository = mazeRepository;
			this.mazeFactory = mazeFactory;
		}

		public async Task GenerateMaze(MazeConfiguration mazeConfiguration)
		{
			Models.Maze maze = mazeFactory.Build(mazeConfiguration);
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