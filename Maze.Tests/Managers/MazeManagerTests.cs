using Maze.Factories;
using Maze.Managers;
using Maze.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Tests.Managers
{
	[TestClass]
	public class MazeManagerTests
	{
		[TestMethod, Description("Test maze manager get method returns correct maze")]
		public async Task TestMazeManagerReturnsCorrectMaze()
		{
			var mockMazeRepository = new Mock<IMazeRepository>();
			mockMazeRepository.Setup(repository => repository.Get(3)).ReturnsAsync(new Models.Maze() { Id = 3 });
			var mockMazeFactory = new Mock<IMazeFactory>();
			var mazeManager = new MazeManager(mockMazeRepository.Object, mockMazeFactory.Object);

			var result = await mazeManager.Get(3);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Id);
		}

		[TestMethod, Description("Test maze manager get method returns null for non existing id")]
		public async Task TestMazeManagerReturnsNullForInvalidId()
		{
			var mockMazeRepository = new Mock<IMazeRepository>();
			mockMazeRepository.Setup(repository => repository.Get(3)).ReturnsAsync(new Models.Maze() { Id = 3 });
			var mockMazeFactory = new Mock<IMazeFactory>();
			var mazeManager = new MazeManager(mockMazeRepository.Object, mockMazeFactory.Object);

			var result = await mazeManager.Get(5);

			Assert.IsNull(result);
		}

		[TestMethod, Description("Test maze manager get method returns null for non existing id")]
		public async Task TestMazeManagerGetAll()
		{
			var mockMazeRepository = new Mock<IMazeRepository>();
			mockMazeRepository.Setup(repository => repository.GetAll()).Returns(new[] { new Models.Maze() { Id = 3 } }.AsQueryable());
			var mockMazeFactory = new Mock<IMazeFactory>();
			var mazeManager = new MazeManager(mockMazeRepository.Object, mockMazeFactory.Object);

			var result = await mazeManager.GetAll();

			Assert.AreEqual(1, result.Count());
		}

		[TestMethod, Description("Test maze manager maze generation calls factory and repository")]
		public async Task TestMazeManagerGenerateMaze()
		{
			var mockMazeRepository = new Mock<IMazeRepository>();
			var mockMazeFactory = new Mock<IMazeFactory>();
			var mazeManager = new MazeManager(mockMazeRepository.Object, mockMazeFactory.Object);

			await mazeManager.GenerateMaze(new Models.MazeConfiguration());

			mockMazeRepository.Verify(repository => repository.Create(It.IsAny<Models.Maze>()), Times.Once);
			mockMazeFactory.Verify(factory => factory.Build(It.IsAny<Models.MazeConfiguration>()), Times.Once);
		}
	}
}