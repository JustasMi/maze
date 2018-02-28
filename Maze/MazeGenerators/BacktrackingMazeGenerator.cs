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
							Bottom = false,
							Top = true,
							Left = false,
							Right = false
						},
						new Cell()
						{
							Bottom = true,
							Top = true,
							Left = false,
							Right = false
						},
						new Cell()
						{
							Bottom = true,
							Top = true,
							Left = false,
							Right = true
						},
					},
					{
						new Cell()
						{
							Bottom = false,
							Top = false,
							Left = true,
							Right = false
						},
						new Cell()
						{
							Bottom = true,
							Top = true,
							Left = false,
							Right = false
						},
						new Cell()
						{
							Bottom = false,
							Top = true,
							Left = false,
							Right = true
						}
					},
					{
						new Cell()
						{
							Bottom = true,
							Top = false,
							Left = true,
							Right = false
						},
						new Cell()
						{
							Bottom = true,
							Top = true,
							Left = false,
							Right = true
						},
						new Cell()
						{
							Bottom = true,
							Top = false,
							Left = true,
							Right = false
						}
					}
				}
			};
		}
	}
}