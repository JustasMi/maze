using Maze.Managers;
using Maze.Models;
using Maze.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Maze.Pages
{
	public class CreateModel : PageModel
	{
		[BindProperty]
		public MazeConfiguration MazeConfiguration { get; set; }

		private readonly IMazeManager mazeManager;

		public CreateModel(IMazeManager mazeManager)
		{
			this.mazeManager = mazeManager;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await this.mazeManager.GenerateMaze(MazeConfiguration);

			return RedirectToPage("/Index");
		}
	}
}