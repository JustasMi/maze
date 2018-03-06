using Maze.Managers;
using Maze.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Maze.Tests.Pages
{
	[TestClass]
	public class CreatePageModelTests
	{
		[TestMethod, Description("Test maze post create calls maze generation manager")]
		public async Task TestCreatePageModelCallsMazeGenerator()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			CreateModel createModel = SetupCreateModel(mockMazeManager.Object);

			await createModel.OnPostAsync();
			mockMazeManager.Verify(manager => manager.GenerateMaze(It.IsAny<Models.MazeConfiguration>()), Times.Once);
		}

		[TestMethod, Description("Test that maze post creates maze and redirects to index page")]
		public async Task TestCreatePageModelCreatesMaze()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			CreateModel createModel = SetupCreateModel(mockMazeManager.Object);

			var result = await createModel.OnPostAsync();
			Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));

			var redirectResult = result as RedirectToPageResult;

			Assert.AreEqual("/Index", redirectResult.PageName);
		}

		[TestMethod, Description("Test maze creator does not create maze when there is validation error")]
		public async Task TestCreatePageModel3()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			CreateModel createModel = SetupCreateModel(mockMazeManager.Object);

			createModel.ModelState.AddModelError("MazeConfiguration.Name", "The maze name is required.");
			var result = await createModel.OnPostAsync();
			Assert.IsInstanceOfType(result, typeof(PageResult));
		}

		private CreateModel SetupCreateModel(IMazeManager mockMazeManager)
		{
			var httpContext = new DefaultHttpContext();
			var modelState = new ModelStateDictionary();
			var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
			var modelMetadataProvider = new EmptyModelMetadataProvider();
			var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
			var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
			var pageContext = new PageContext(actionContext)
			{
				ViewData = viewData
			};

			return new CreateModel(mockMazeManager)
			{
				MazeConfiguration = new Models.MazeConfiguration(),
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext)
			};
		}
	}
}