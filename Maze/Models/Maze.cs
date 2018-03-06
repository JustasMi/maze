using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maze.Models
{
	public class Maze
	{
		public int Id { get; set; }
		public MazeConfiguration Configuration { get; set; }

		[JsonIgnore]
		public string cells { get; set; } // TODO: find a better way of storing serialized values

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
}