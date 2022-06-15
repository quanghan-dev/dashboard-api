using Serilog;
using Dashboard.API.Configurations;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Dashboard.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//add extensions
builder.Host.AddConfigurations();

//logging
builder.Host.UseSerilog((_, config) => {
  config.WriteTo.Console()
  .ReadFrom.Configuration(configuration);
});

//add CORS
builder.Services.AddCors(opt => {
  opt.AddPolicy(name: "MyPolicy", builder => {
    builder.WithOrigins(configuration.GetValue<string>("CoreSettings:Backend"))
           .AllowAnyHeader()
           .AllowAnyMethod();
  });
});

//add database
builder.Services.AddDbContext<DashboardContext>(opt => {
  opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreDB"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dashboard", Version = "v1" });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
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

builder.Services.AddAuthentication(x => {
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
  x.Audience = "api1";
  x.Authority = "http://localhost:7285";
  x.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add CORS
app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
