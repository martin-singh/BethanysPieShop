using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Host is behing set up, which will configure a server and a request processing pipeline.
            CreateHostBuilder(args).Build().Run();
        }

        // Will setup the app with some defaults, which is hosted by its internal server enabled by ISS.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // Startup -> configuration of the app.
                });
    }
}
