using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maze.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maze.Pages
{
	public class CreateModel : PageModel
	{
		private readonly IMazeRepository mazeRepository;

		public CreateModel(IMazeRepository mazeRepository)
		{
			this.mazeRepository = mazeRepository;
		}

		public void OnGet()
		{
		}
	}
}