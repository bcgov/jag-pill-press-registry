using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Gov.Jag.PillPressRegistry.Public.Test
{
    #region snippet1
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<Gov.Jag.PillPressRegistry.Public.Startup>
    {
        public IConfiguration Configuration;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Configuration = new ConfigurationBuilder()
                                .AddUserSecrets<Gov.Jag.PillPressRegistry.Public.Startup>()
                                .AddEnvironmentVariables()
                                .Build();

            builder
            .UseEnvironment("Staging")
            .UseConfiguration(Configuration)
            .UseStartup<Gov.Jag.PillPressRegistry.Public.Startup>();
        }
    }
    #endregion
}
