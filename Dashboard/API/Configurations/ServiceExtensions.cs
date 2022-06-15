namespace API.Configurations {
  internal static class ServiceExtensions {
    public static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host) {
      host.ConfigureAppConfiguration((context, config) => {
        const string configurationsDirectory = "Configurations";
        var env = context.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"{configurationsDirectory}/cors.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();
      });
      return host;
    }
  }
}
