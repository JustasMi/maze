using Maze.Managers;
using Maze.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Tests.Pages
{
	[TestClass]
	public class IndexPageModelTests
	{
		[TestMethod, Description("Test get method setting page model")]
		public async Task TestIndexPageModel()
		{
			Mock<IMazeManager> mockMazeManager = new Mock<IMazeManager>();
			mockMazeManager.Setup(manager => manager.GetAll()).ReturnsAsync(new[] { new Models.Maze() { Id = 3 } });
			IndexModel indexModel = new IndexModel(mockMazeManager.Object);
			indexModel.OnGetAsync();

			var pageModelMazes = indexModel.Mazes;

			Assert.IsNotNull(pageModelMazes);
			Assert.AreEqual(1, pageModelMazes.Count());
			Assert.AreEqual(3, pageModelMazes.Single().Id);
		}
	}
}