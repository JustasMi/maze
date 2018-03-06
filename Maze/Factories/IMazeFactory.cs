using Maze.Models;

namespace Maze.Factories
{
	public interface IMazeFactory
	{
		Models.Maze Build(MazeConfiguration mazeConfiguration);
	}
}