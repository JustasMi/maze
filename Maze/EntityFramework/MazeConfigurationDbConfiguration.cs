using Maze.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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