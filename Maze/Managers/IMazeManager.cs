﻿using Maze.Models;
using System.Threading.Tasks;

namespace Maze.Managers
{
	public interface IMazeManager
	{
		Task GenerateMaze(MazeConfiguration mazeConfiguration);

		Task<Models.Maze[]> GetAll();

		Task<Models.Maze> Get(int id);
	}
}