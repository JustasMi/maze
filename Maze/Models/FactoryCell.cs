using System.Collections.Generic;

namespace Maze.Models
{
	public class FactoryCell : Cell
	{
		public bool Visited { get; set; }
		public IList<NeighbourCell> Neighbours { get; set; }
	}
}