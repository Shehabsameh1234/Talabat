using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace AminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region configure services
            // Add services to the container.
            webApplicationBuilder.Services.AddControllersWithViews();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options/*.UseLazyLoadingProxies()*/.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });
            //add identity services
            webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.Password = null;
            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            #endregion

            var app = webApplicationBuilder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}