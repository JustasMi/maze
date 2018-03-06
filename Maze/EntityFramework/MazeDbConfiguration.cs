using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maze.EntityFramework
{
	public class MazeDbConfiguration : IEntityTypeConfiguration<Models.Maze>
	{
		public void Configure(EntityTypeBuilder<Models.Maze> builder)
		{
			builder.HasKey(maze => maze.Id);
		}
	}
}