using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Repository.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

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
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			//about swagger
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			webApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
			{
				options/*.UseLazyLoadingProxies()*/.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});
			//apply service for generic repos
			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));
			webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
											.SelectMany(p => p.Value.Errors)
											.Select(p => p.ErrorMessage)
											.ToList();
					var response = new ApisValidationErrors()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(response);
				};
			});
			#endregion
			
			var app = webApplicationBuilder.Build();

			#region Asking Clr To Generate Object From storeContext
			//1-Create scope (using keyword => dispose the scope after using it )
			using var scope = app.Services.CreateScope();

			//2-Create service
			var service = scope.ServiceProvider;

			//3-generate object from StoreContext
			var _DbContext=service.GetRequiredService<StoreContext>();

			//4- log the ex using loggerFactory Class and generate object from loggerFactory
			var loggerFactory =service.GetRequiredService<ILoggerFactory>();

			try
			{
				//4-add migration
				await _DbContext.Database.MigrateAsync();
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
				app.UseSwagger();
				app.UseSwaggerUI();

				//it works internally
				//app.UseDeveloperExceptionPage();
			}

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
