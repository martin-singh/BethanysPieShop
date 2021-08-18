using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysPieShop
{
    // Startup -> 2 things mainly: Define the request handling pipeline and configure all services needed thru out the app.
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register services here (through dependency injection):

            // Framework services...
            services.AddControllersWithViews(); // Bring in support for working with MVC.

            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // Local DB

            //Our own services...
            services.AddScoped<IPieRepository, PieRepository>(); // AddScoped -> creates one instance per HTTP-request.
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddHttpContextAccessor();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add middleware components here (order matters):
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); // Redirects HTTP-requests to HTTPS.

            app.UseStaticFiles(); // Serves static files such as JS, CSS, images etc in the wwwroot-folder.

            app.UseSession(); // For sessions.

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
