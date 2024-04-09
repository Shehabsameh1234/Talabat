namespace Talabat.APIs
{
	public class Program
	{
		//Entry Point
		public static void Main(string[] args)
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
			#endregion
			

			var app = webApplicationBuilder.Build();

			#region Configure Kestrel MiddleWares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

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
