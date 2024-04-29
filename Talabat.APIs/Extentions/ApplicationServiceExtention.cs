using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Srevice.AuthService;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            //apply service for generic repos
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //add DI for auth service to add token
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

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
    }
}
