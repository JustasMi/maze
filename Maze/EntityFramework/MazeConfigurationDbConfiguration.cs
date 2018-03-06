using Maze.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maze.EntityFramework
{
	public class MazeConfigurationDbConfiguration : IEntityTypeConfiguration<MazeConfiguration>
	{
		public void Configure(EntityTypeBuilder<MazeConfiguration> builder)
		{
			builder.HasKey(configuration => configuration.Id);
		}
	}
}