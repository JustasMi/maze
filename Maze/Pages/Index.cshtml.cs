using System.Collections.Generic;
using Maze.Managers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maze.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IMazeManager mazeManager;

		public IEnumerable<Models.Maze> Mazes { get; private set; }

		public IndexModel(IMazeManager mazeManager)
		{
			this.mazeManager = mazeManager;
		}

		public async void OnGetAsync()
		{
			Mazes = await this.mazeManager.GetAll();
		}
	}
}