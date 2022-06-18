using Serilog;
using API.Configurations;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.Persistence;
using API.Filters;
using FluentValidation.AspNetCore;
using Application.Models.Validators;
using API.Middleware;

#region Configuration
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Host.AddConfigurations();
builder.Services.AddApplicationServices(configuration);
#endregion

#region Logging
//logging
builder.Host.UseSerilog((_, config) =>
{
    config.WriteTo.Console()
    .ReadFrom.Configuration(configuration);
});
#endregion

#region Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "MyPolicy", builder =>
    {
        builder.WithOrigins(configuration.GetValue<string>("CorsSettings:Backend"))
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
#endregion

#region Database
builder.Services.AddDbContext<DashboardContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDB"));
});
#endregion

#region Controller
builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidateModelAttribute))
    )
    .AddFluentValidation(
        options => options.RegisterValidatorsFromAssemblyContaining<IValidationsMarker>()
    );

builder.Services.AddEndpointsApiExplorer();
#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dashboard", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
      {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          },
          Array.Empty<string>()
      }
  });
});
#endregion


#region App Settings
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dashboard v1");
    });
}

app.UseHttpsRedirection();

//add CORS
app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
#endregion