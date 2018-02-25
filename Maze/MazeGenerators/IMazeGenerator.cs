using Maze.Models;

namespace Maze.MazeGenerators
{
	public interface IMazeGenerator
	{
		Models.Maze Generate(MazeConfiguration mazeConfiguration);
	}
}