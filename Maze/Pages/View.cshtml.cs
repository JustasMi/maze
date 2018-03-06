using System.Threading.Tasks;
using Maze.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Maze.Pages
{
	public class ViewModel : PageModel
	{
		private readonly IMazeManager mazeManager;

		[BindProperty]
		public Models.Maze Maze { get; set; }

		public ViewModel(IMazeManager mazeManager)
		{
			this.mazeManager = mazeManager;
		}

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Maze = await mazeManager.Get(id);

			if (Maze == null)
			{
				return RedirectToPage("/Index");
			}

			return Page();
		}

		public IActionResult OnGetMaze(int id)
		{
			return new JsonResult(JsonConvert.SerializeObject(mazeManager.Get(id)));
		}
	}
}