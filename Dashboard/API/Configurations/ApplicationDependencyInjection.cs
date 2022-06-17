using Application.Common.Email;
using Application.Services;
using Application.Services.Impl;
using DataAccess.UnitOfWork;
using DataAccess.UnitOfWork.Impl;

namespace API.Configurations
{
    internal static class ConfigExtensions
    {
        public static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                const string configurationsDirectory = "Configurations";
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"{configurationsDirectory}/cors.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"{configurationsDirectory}/database.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"{configurationsDirectory}/mail.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();
            });
            return host;
        }
    }

    internal static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(config.GetSection("SmtpSettings").Get<SmtpSettings>());

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUtilService, UtilService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
