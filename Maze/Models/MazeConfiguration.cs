using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Models
{
	public class MazeConfiguration
	{
		public int Height { get; set; }
		public int Width { get; set; }

		[Required, StringLength(25)]
		public string Name { get; set; }
	}
}