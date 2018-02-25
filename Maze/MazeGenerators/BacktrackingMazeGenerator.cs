using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maze.Models;

namespace Maze.MazeGenerators
{
	public class BacktrackingMazeGenerator : IMazeGenerator
	{
		public Models.Maze Generate(MazeConfiguration mazeConfiguration)
		{
			return new Models.Maze()
			{
				Configuration = mazeConfiguration,
				Cells = new Cell[,]
				{
					{
						new Cell()
						{
							Border = Border.East | Border.North
						},
						new Cell()
						{
							Border = Border.South | Border.North
						},
						new Cell()
						{
						},
					},
					{
						new Cell()
						{
						},
						new Cell()
						{
						},
						new Cell()
						{
						}
					},
					{
						new Cell()
						{
						},
						new Cell()
						{
						},
						new Cell()
						{
						}
					}
				}
			};
		}
	}
}