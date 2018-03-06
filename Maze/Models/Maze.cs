using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maze.Models
{
	public class Maze
	{
		public int Id { get; set; }
		public MazeConfiguration Configuration { get; set; }

		[JsonIgnore]
		public string SerializedCells { get; set; }

		[NotMapped]
		public Cell[,] Cells
		{
			get
			{
				return JsonConvert.DeserializeObject<Cell[,]>(SerializedCells);
			}
			set
			{
				SerializedCells = JsonConvert.SerializeObject(value);
			}
		}
	}
}