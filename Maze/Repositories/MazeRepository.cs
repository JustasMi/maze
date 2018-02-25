using Maze.Models;
using System.Threading.Tasks;

namespace Maze.Repositories
{
	public class MazeRepository : IMazeRepository
	{
		private readonly MazeDbContext context;

		public MazeRepository(MazeDbContext context)
		{
			this.context = context;
		}

		public async Task Create(Models.Maze entity)
		{
			await context.Maze.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public Task<Models.Maze> Get(int key)
		{
			return context.Maze.FindAsync(key);
		}

		#region IDisposable Support

		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					context.Dispose();
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}

		#endregion IDisposable Support
	}
}