using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop
{
    // Startup -> 2 things mainly: Define the request handling pipeline and configure all services needed thru out the app.
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register services here (through dependency injection):

            // Framework services...
            services.AddControllersWithViews(); // Bring in support for working with MVC.

            //Our own services...
            services.AddScoped<IPieRepository, MockPieRepository>(); // AddScoped -> creates one instance per HTTP-request.
            services.AddScoped<ICategoryRepository, MockCategoryRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add middleware components here (order matters):
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); // Redirects HTTP-requests to HTTPS.

            app.UseStaticFiles(); // Serves static files such as JS, CSS, images etc.
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Responsible to map a incoming request to a action of a controller.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
