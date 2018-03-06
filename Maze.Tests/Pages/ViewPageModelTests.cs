using Maze.Managers;
using Maze.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Maze.Tests.Pages
{
	[TestClass]
	public class ViewPageModelTests
	{
		[TestMethod, Description("Test get method setting page model")]
		public async Task TestViewPageModelWithValidMaze()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			mockMazeManager.Setup(manager => manager.Get(It.IsAny<int>())).ReturnsAsync(new Models.Maze() { Id = 3 });
			var viewModel = new ViewModel(mockMazeManager.Object);
			var actionResult = await viewModel.OnGetAsync(3);

			Assert.IsNotNull(viewModel.Maze);
			Assert.AreEqual(3, viewModel.Maze.Id);
			Assert.IsInstanceOfType(actionResult, typeof(PageResult));
		}

		[TestMethod, Description("Test get when maze with given id does not exist")]
		public async Task TestViewPageModelWhenMazeIsNull()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			mockMazeManager.Setup(manager => manager.Get(It.IsAny<int>())).Returns(Task.FromResult<Models.Maze>(null));
			ViewModel viewModel = new ViewModel(mockMazeManager.Object);
			IActionResult actionResult = await viewModel.OnGetAsync(5);

			Assert.IsNull(viewModel.Maze);
			Assert.IsInstanceOfType(actionResult, typeof(RedirectToPageResult));

			var redirect = actionResult as RedirectToPageResult;
			Assert.AreEqual("/Index", redirect.PageName);
		}

		[TestMethod, Description("Test that maze handler returns JSON result")]
		public void TestViewPageModeMazeHandler()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			mockMazeManager.Setup(manager => manager.Get(It.IsAny<int>())).ReturnsAsync(new Models.Maze() { Cells = new Models.Cell[,] { } });
			var viewModel = new ViewModel(mockMazeManager.Object);
			var actionResult = viewModel.OnGetMaze(3);
			Assert.IsInstanceOfType(actionResult, typeof(JsonResult));
		}
	}
}