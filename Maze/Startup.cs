using Maze.EntityFramework;
using Maze.Managers;
using Maze.MazeGenerators;
using Maze.Repositories;
using Maze.Validation;
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

			services.AddScoped<MazeConfigurationValidator>();
			services.AddScoped<MazeNameValidator>();

			services.AddScoped<IMazeManager, MazeManager>();

			services.AddScoped<IMazeGenerator, BacktrackingMazeGenerator>();
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