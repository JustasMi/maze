using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maze.Models;

namespace Maze.Factories
{
	public class MazeFactory : IMazeFactory
	{
		private readonly IMapper mapper;

		public MazeFactory(IMapper mapper)
		{
			this.mapper = mapper;
		}

		public Models.Maze Build(MazeConfiguration mazeConfiguration)
		{
			FactoryCell[,] cells = InitialiseGrid(mazeConfiguration);
			Random random = new Random();
			Stack<FactoryCell> cellStack = new Stack<FactoryCell>();

			FactoryCell currentCell = cells[0, 0];
			currentCell.Left = false;
			currentCell.Visited = true;

			bool hasUnvisitedCells = true;

			while (hasUnvisitedCells)
			{
				var unvisitedNeighbours = currentCell.Neighbours
					.Where(n => !n.Cell.Visited)
					.ToList();
				int unvisitedNeighbourCount = unvisitedNeighbours.Count();

				if (unvisitedNeighbourCount > 0)
				{
					int randomIndex = random.Next(unvisitedNeighbourCount);
					var selectedNeighbour = unvisitedNeighbours[randomIndex];

					cellStack.Push(currentCell);

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

				foreach (FactoryCell cell in cells)
				{
					if (!cell.Visited)
					{
						hasUnvisitedCells = true;
						break;
					}
				}
			}

			FactoryCell finishCell = cells[mazeConfiguration.Height - 1, mazeConfiguration.Width - 1];
			finishCell.Right = false;
			finishCell.Goal = true;

			return new Models.Maze()
			{
				Cells = this.mapper.Map<Cell[,]>(cells),
				Configuration = mazeConfiguration
			};
		}

		private FactoryCell[,] InitialiseGrid(MazeConfiguration mazeConfiguration)
		{
			int boundaryX = mazeConfiguration.Width;
			int boundaryY = mazeConfiguration.Height;

			FactoryCell[,] cells = new FactoryCell[boundaryY, boundaryX];
			for (int i = 0; i < boundaryY; i++)
			{
				for (int j = 0; j < boundaryX; j++)
				{
					cells[i, j] = new FactoryCell()
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
					// Consider right neighbour
					if (IsValidNeighbour(i, j + 1, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i, j + 1],
							Direction = NeighbourDirection.Right
						});
					}
					// Consider left neighbour
					if (IsValidNeighbour(i, j - 1, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i, j - 1],
							Direction = NeighbourDirection.Left
						});
					}
					// Consider top neighbour
					if (IsValidNeighbour(i - 1, j, boundaryX, boundaryY))
					{
						neighbours.Add(new NeighbourCell()
						{
							Cell = cells[i - 1, j],
							Direction = NeighbourDirection.Top
						});
					}
					// Consider bottom neighbour
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
	}
}