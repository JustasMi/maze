using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maze.Models
{
	public class MazeDbConfiguration : IEntityTypeConfiguration<Maze>
	{
		public void Configure(EntityTypeBuilder<Maze> builder)
		{
			builder.HasKey(maze => maze.Id);
		}
	}
}