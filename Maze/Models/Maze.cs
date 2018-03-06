using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maze.Models
{
	public class Maze
	{
		public int Id { get; set; }
		public MazeConfiguration Configuration { get; set; }

		[JsonIgnore]
		public string cells { get; set; } // do something about this

		[NotMapped]
		public Cell[,] Cells
		{
			get
			{
				return JsonConvert.DeserializeObject<Cell[,]>(cells);
			}
			set
			{
				cells = JsonConvert.SerializeObject(value);
			}
		}
	}

	public class Cell
	{
		public bool Left { get; set; }
		public bool Right { get; set; }
		public bool Top { get; set; }
		public bool Bottom { get; set; }
		public bool Goal { get; set; }
	}

	[Flags]
	public enum Border
	{
		None = 0,
		North = 1 << 0,
		East = 1 << 1,
		South = 1 << 2,
		West = 1 << 3
	}
}