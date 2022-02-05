# Egonsoft.HU Configuration Extensions
[![GitHub](https://img.shields.io/github/license/gcsizmadia/EgonsoftHU.Extensions.Bcl?label=License)](https://opensource.org/licenses/MIT)

Extensions for Microsoft.Extensions.Configuration.

## EgonsoftHU.Extensions.Configuration.ConfigurationManager
[![Nuget](https://img.shields.io/nuget/v/EgonsoftHU.Extensions.Configuration.ConfigurationManager?label=NuGet)](https://www.nuget.org/packages/EgonsoftHU.Extensions.Configuration.ConfigurationManager)
[![Nuget](https://img.shields.io/nuget/dt/EgonsoftHU.Extensions.Configuration.ConfigurationManager?label=Downloads)](https://www.nuget.org/packages/EgonsoftHU.Extensions.Configuration.ConfigurationManager)

App.config/Web.config configuration provider implementation for Microsoft.Extensions.Configuration.

### Introduction
When migrating from .NET Framework to .NET Core and your `App.config` or `Web.config` file contains 
lots of settings it might be desirable to be able to incrementally transfer those settings into the 
new `appsettings.json` file so that you can incrementally replace the static uses of [`ConfigurationManager`](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager) with 
an injectable [`IConfiguration`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration) instance.

### Releases
You can download the package from [nuget.org](https://www.nuget.org/).
- [EgonsoftHU.Extensions.Configuration.ConfigurationManager](https://www.nuget.org/packages/EgonsoftHU.Extensions.Configuration.ConfigurationManager)

You can find the release notes [here](https://github.com/gcsizmadia/EgonsoftHU.Extensions.Configuration/releases).

### Usage
Suppose you have an `App.config` file with the following content.
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <!-- legacy setting -->
    <add key="MyAppSetting" value="..." />
    <!--
    Temporarily added to this file during migration.
    The key uses the new format so that 
    later it can be read the same way from appsettings.json file.
    -->
    <add key="SomeApi:ApiKey" value="..." />
  </appSettings>
  <connectionStrings>
    <add name="MyConnectionString" connectionString="..." />
  </connectionStrings>
  <!-- The rest is omitted for clarity. -->
</configuration>
```
First you need to add the configuration provider.
```C#
using Microsoft.Extensions.Configuration;

private static IHostBuilder CreateHostBuilder(string[] args)
{
    return
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                }
            )
            .ConfigureAppConfiguration(
                (hostBuilderContext, configurationBuilder) =>
                {
                    // Add the configuration provider here.
                    configurationBuilder.AddConfigurationManager();
                }
            );
}
```
Then you can read the settings using the injected `IConfiguration` instance, e.g. in the `Startup.cs` file.
```C#
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Get the legacy app setting.
        string myAppSetting = Configuration["MyAppSetting"];

        // Get the new setting
        string apiKey1 = Configuration.GetSection("SomeApi")["ApiKey"];

        // Alternatively you can get the new setting also this way.
        string apiKey2 = Configuration["SomeApi:ApiKey"];

        // Get the connection string.
        string myConnectionString1 = Configuration.GetConnectionString("MyConnectionString");

        // Alternatively you can get the connection string also this way.
        string myConnectionString2 = Configuration["ConnectionStrings:MyConnectionString");

        // The rest is omitted for clarity.
    }
}
```
### Credits
Ben Foster
- Blogpost: [Using .NET Core Configuration with legacy projects](https://benfoster.io/blog/net-core-configuration-legacy-projects/)
- Contact: [GitHub](https://github.com/benfoster), [Twitter](https://twitter.com/benfosterdev)