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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maze
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

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

			JsonConvert.DefaultSettings = (() =>
			{
				JsonSerializerSettings settings = new JsonSerializerSettings();
				settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				return settings;
			});
		}

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