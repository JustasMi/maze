using Microsoft.EntityFrameworkCore;

namespace Maze.EntityFramework
{
	public class MazeDbContext : DbContext
	{
		public MazeDbContext(DbContextOptions<MazeDbContext> options)
			: base(options)
		{
		}

		public DbSet<Models.Maze> Maze { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new MazeDbConfiguration());
		}
	}
}