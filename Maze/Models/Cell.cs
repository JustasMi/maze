namespace Maze.Models
{
	public class Cell
	{
		public bool Left { get; set; }
		public bool Right { get; set; }
		public bool Top { get; set; }
		public bool Bottom { get; set; }
		public bool Goal { get; set; }
	}
}