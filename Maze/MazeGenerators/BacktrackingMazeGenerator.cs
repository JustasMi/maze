using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maze.Models;

namespace Maze.MazeGenerators
{
	public class BacktrackingMazeGenerator : IMazeGenerator
	{
		private readonly IMapper mapper;

		public BacktrackingMazeGenerator(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public Models.Maze Generate(MazeConfiguration mazeConfiguration)
		{
			BuildCell[,] cells = InitialiseCells(mazeConfiguration);
			Random random = new Random();
			Stack<BuildCell> cellStack = new Stack<BuildCell>();
			// start algorithm
			// initial cell
			var currentCell = cells[0, 0];
			currentCell.Left = false;
			currentCell.Visited = true;

			bool hasUnvisitedCells = true;

			while (hasUnvisitedCells)
			{
				// find random unvisited neighbour
				var unvisitedNeighbours = currentCell.Neighbours
					.Where(n => !n.Cell.Visited)
					.ToList();
				int unvisitedNeighbourCount = unvisitedNeighbours.Count();

				if (unvisitedNeighbourCount > 0)
				{
					int randomIndex = random.Next(unvisitedNeighbourCount);
					var selectedNeighbour = unvisitedNeighbours[randomIndex];

					// push cell to stack
					cellStack.Push(currentCell);

					// remove wall
					if (selectedNeighbour.Direction == NeighbourDirection.Bottom)
					{
						currentCell.Bottom = false;
						selectedNeighbour.Cell.Top = false;
					}
					else if (selectedNeighbour.Direction == NeighbourDirection.Top)
					{
						currentCell.Top = false;
						selectedNeighbour.Cell.Bottom = false;
					}
					else if (selectedNeighbour.Direction == NeighbourDirection.Left)
					{
						currentCell.Left = false;
						selectedNeighbour.Cell.Right = false;
					}
					else
					{
						currentCell.Right = false;
						selectedNeighbour.Cell.Left = false;
					}
					// mark chosen cell as visited and select as current one
					selectedNeighbour.Cell.Visited = true;
					currentCell = selectedNeighbour.Cell;
				}
				else
				{
					if (cellStack.Any())
					{
						currentCell = cellStack.Pop();
					}
				}

				hasUnvisitedCells = false;
				// check if there are unvisited cells
				foreach (BuildCell b in cells)
				{
					if (!b.Visited)
					{
						hasUnvisitedCells = true;
						break;
					}
				}
			}
			BuildCell finishCell = cells[mazeConfiguration.Height - 1, mazeConfiguration.Width - 1];
			finishCell.Right = false;
			finishCell.Goal = true;

			return new Models.Maze()
			{
				Cells = this.mapper.Map<Cell[,]>(cells),
				Configuration = mazeConfiguration
			};
		}

		private BuildCell[,] InitialiseCells(MazeConfiguration mazeConfiguration)
		{
			int boundaryX = mazeConfiguration.Width;
			int boundaryY = mazeConfiguration.Height;

			BuildCell[,] cells = new BuildCell[boundaryY, boundaryX];
			for (int i = 0; i < boundaryY; i++)
			{
				for (int j = 0; j < boundaryX; j++)
				{
					cells[i, j] = new BuildCell()
					{
						Bottom = true,
						Left = true,
						Right = true,
						Top = true,
						Visited = false
					};
				}
			}

			for (int i = 0; i < boundaryY; i++)
			{
				for (int j = 0; j < boundaryX; j++)
				{
					List<NeighbourCell> neighbours = new List<NeighbourCell>();
					// consider right
					if (IsValidNeighbour(i, j + 1, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i, j + 1],
							Direction = NeighbourDirection.Right
						});
					}
					// consider left
					if (IsValidNeighbour(i, j - 1, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i, j - 1],
							Direction = NeighbourDirection.Left
						});
					}
					// consider up
					if (IsValidNeighbour(i - 1, j, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i - 1, j],
							Direction = NeighbourDirection.Top
						});
					}
					// consider bottom
					if (IsValidNeighbour(i + 1, j, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i + 1, j],
							Direction = NeighbourDirection.Bottom
						});
					}

					cells[i, j].Neighbours = neighbours;
				}
			}

			return cells;
		}

		private bool IsValidNeighbour(int neighbourY, int neighbourX, int boundaryX, int boundaryY)
		{
			return neighbourX >= 0 && neighbourX < boundaryX && neighbourY >= 0 && neighbourY < boundaryY;
		}

		private BuildCell FindUnvisitedNeighbours()
		{
			return null;
		}
	}

	public class BuildCell : Cell
	{
		public bool Visited { get; set; }
		public IList<NeighbourCell> Neighbours { get; set; }
	}

	public class NeighbourCell
	{
		public BuildCell Cell { get; set; }
		public NeighbourDirection Direction { get; set; }
	}

	public enum NeighbourDirection
	{
		None = 0,
		Left = 1,
		Right = 2,
		Top = 3,
		Bottom = 4
	}
}