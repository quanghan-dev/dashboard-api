using System.Text;
using System.Text.Json;
using Application.Common.Email;
using Application.Models;
using Application.Services;
using Application.Services.Impl;
using DataAccess.UnitOfWork;
using DataAccess.UnitOfWork.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
                      .AddJsonFile($"{configurationsDirectory}/jwt.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"{configurationsDirectory}/cache.json", optional: false, reloadOnChange: true)
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

            #region Redis
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = config.GetValue<string>("CacheSettings:Redis");
            });
            #endregion

            #region Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(config.GetSection("SmtpSettings").Get<SmtpSettings>());

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUtilService, UtilService>();
            services.AddScoped<IEmailService, EmailService>();
            #endregion

            #region Authentication
            var key = Encoding.ASCII.GetBytes(config.GetValue<string>("Jwt:Custom:Key"));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            #endregion

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
