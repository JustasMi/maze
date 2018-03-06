using AutoMapper;
using Maze.EntityFramework;
using Maze.Factories;
using Maze.Managers;
using Maze.Models;
using Maze.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maze
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDbContext<MazeDbContext>(options => options.UseInMemoryDatabase("Maze"));
			services.AddScoped<IMazeRepository, MazeRepository>();
			services.AddScoped<IMazeManager, MazeManager>();
			services.AddScoped<IMazeFactory, MazeFactory>();
			services.AddSingleton<IMapper>(serviceProvider =>
			{
				IMapper mapper = new MapperConfiguration(configuration =>
				{
					configuration.CreateMap<FactoryCell, Cell>();
				}).CreateMapper();
				return mapper;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseMvc();
		}
	}
}