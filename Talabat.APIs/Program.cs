using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Repository.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
	public class Program
	{
		//Entry Point
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);


			#region Configure services
			// Add services to the container.

			//register required web apis  services to the DI container
			webApplicationBuilder.Services.AddControllers();
			
			

			webApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
			{
				options/*.UseLazyLoadingProxies()*/.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			//my own extention method
			webApplicationBuilder.Services.ApplicationServices().SwaggerServices();

			//redis objct
			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvidor=>
			{
				var Connection = webApplicationBuilder.Configuration.GetConnectionString("redisConnection");
                return ConnectionMultiplexer.Connect(Connection);
			});
			webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
			{
				options/*.UseLazyLoadingProxies()*/.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
			});
			
			#endregion
			
			var app = webApplicationBuilder.Build();

			#region Asking Clr To Generate Object From storeContext
			//1-Create scope (using keyword => dispose the scope after using it )
			using var scope = app.Services.CreateScope();

			//2-Create service
			var service = scope.ServiceProvider;

            //3-generate object from StoreContext and _IdentityDbContext
            var _DbContext =service.GetRequiredService<StoreContext>();
            var _IdentityDbContext = service.GetRequiredService<ApplicationIdentityDbContext>();

            //4- log the ex using loggerFactory Class and generate object from loggerFactory
            var loggerFactory =service.GetRequiredService<ILoggerFactory>();
			try
			{
				//4-add migration
				await _DbContext.Database.MigrateAsync();
                await _IdentityDbContext.Database.MigrateAsync();

                //-data seeding
                await StoreContextSeed.SeedAsync(_DbContext);
			}
			catch (Exception ex )
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during apply the migration");
			}

			#endregion


			#region Configure Kestrel MiddleWares

			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
                //my own extention method
                app.SwaggerMiddleWares();

                //it works internally
                //app.UseDeveloperExceptionPage();
            }
			//handling when the user tryng to reach endpoind not existed
			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			////in mvc
			//app.UseRouting();
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllerRoute(
			//		name: "default",
			//		pattern: "{controller}/{action}/{id?}"
			//		);
			//});


			//to use route debend on attr route
			app.MapControllers();

			
			#endregion
			

			app.Run();
		}
	}
}
