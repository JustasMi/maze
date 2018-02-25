using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Models
{
	public class Maze
	{
		public int Id { get; set; }

		public MazeConfiguration Configuration { get; set; }
	}
}