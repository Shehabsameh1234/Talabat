using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Srevice.AuthService;
using Talabat.Srevice.OrderService;
using Talabat.Srevice.PaymentService;
using Talabat.Srevice.ProductService;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));


			services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(IProductService), typeof(ProductService));

            services.AddScoped(typeof(IBasektRepository), typeof(BasketRepository));

            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
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

            return services;
        }
        public static IServiceCollection AddAuthServicees(this IServiceCollection services,IConfiguration configuration)
        {

            //add auth service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerOptions =>
                {
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["jwt:validIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["jwt:validAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:AuthKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            //add DI for auth service to add token
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            return services;
        }
    }
}
