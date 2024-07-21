using FluentValidation;
using FluentValidation.AspNetCore;
using MeteoriteAPI.Application.Clients;
using MeteoriteAPI.Application.Clients.Interfaces;
using MeteoriteAPI.Application.Services;
using MeteoriteAPI.Application.Services.Interfaces;
using MeteoriteAPI.Domain;
using MeteoriteAPI.Domain.Repositories;
using MeteoriteAPI.Domain.Repositories.Interfaces;
using MeteoriteAPI.Infrastructure.Models.Options;
using MeteoriteAPI.Web.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MeteoriteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var servises = builder.Services;
            var configuration = builder.Configuration;

            configuration.AddJsonFile("appsettings.json");

            servises.Configure<NasaClientOptions>(builder.Configuration.GetSection(NasaClientOptions.SectionPath));

            ConfigureMappingProfiles(servises);
            ConfigureDatabase(servises, configuration);

            ConfigureServices(servises);
            ConfigureClients(servises);
            ConfigureRepositories(servises);

            servises.AddControllers();

            ConfigureValidation(servises);

            var app = builder.Build();

            app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureMappingProfiles(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Web.Mappings.MeteoriteMappingProfile).Assembly);
            services.AddAutoMapper(typeof(Application.Mappings.MeteoriteMappingProfile).Assembly);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IMeteoriteService, MeteoriteService>();
            services.AddTransient<IMeteoriteProcessingService, MeteoriteProcessingService>();
        }

        private static void ConfigureClients(IServiceCollection services)
        {
            services.AddHttpClient<INasaClient, NasaClient>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IMeteoriteRepository, MeteoriteRepository>();
        }

        private static void ConfigureDatabase(IServiceCollection services, ConfigurationManager configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeteoritesContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MeteoritesContext>(
                options =>
                options.UseSqlServer(connectionString, _ => _.MigrationsAssembly("MeteoriteAPI.Domain.Migrations")));
        }

        private static void ConfigureValidation(IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining(typeof(MeteoriteFilterValidator));
        }
    }
}
