using System.ComponentModel.DataAnnotations;

namespace Maze.Models
{
	public class MazeConfiguration
	{
		public int Id { get; set; }

		public int Height { get; set; }
		public int Width { get; set; }

		[Required, StringLength(25)]
		public string Name { get; set; }
	}
}