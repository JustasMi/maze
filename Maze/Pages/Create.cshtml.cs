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

		private readonly IMazeRepository mazeRepository;

		public CreateModel(IMazeRepository mazeRepository)
		{
			this.mazeRepository = mazeRepository;
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

			return RedirectToPage("/Index");
		}
	}
}