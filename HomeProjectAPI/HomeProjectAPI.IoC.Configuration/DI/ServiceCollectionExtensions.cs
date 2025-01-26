using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HomeProjectAPI.Services;
using HomeProjectAPI.Services.Contracts;
using HomeProjectAPI.Services.Contracts.Services;
using HomeProjectAPI.Services.Model;
using HomeProjectAPI.Services.Services;
using HomeProjectAPI.Services.Validators;

namespace HomeProjectAPI.IoC.Configuration.DI
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services != null)
            {
                services.AddTransient<IUserService, UserService>();
                services.AddScoped<INoteService, NoteService>();
            }
        }

        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            //TO BE COMPLETED IF NEEDED
        }

        public static void ConfigureMappings(this IServiceCollection services)
        {
            if (services != null)
            {
                //Automap settings
                services.AddAutoMapper(Assembly.GetExecutingAssembly());
            }
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            if (services != null)
            {
                services.AddScoped<IValidator<UserCreation>, UserCreationValidation>();
                services.AddScoped<IValidator<User>, UserValidator>();
            }
        }
    }
}