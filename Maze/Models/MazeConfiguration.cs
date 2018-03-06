using System.ComponentModel.DataAnnotations;

namespace Maze.Models
{
	public class MazeConfiguration
	{
		public int Id { get; set; }
		public int CellSize { get; set; } = 30;

		[Required]
		[Range(10, 25)]
		public int Height { get; set; }

		[Required]
		[Range(10, 25)]
		public int Width { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 5)]
		public string Name { get; set; }
	}
}