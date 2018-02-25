﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maze.Models
{
	public class MazeDbContext : DbContext
	{
		public MazeDbContext(DbContextOptions<MazeDbContext> options)
			: base(options)
		{
		}

		public DbSet<Maze> Maze { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new MazeDbConfiguration());
		}
	}
}