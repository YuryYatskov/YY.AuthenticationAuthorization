using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Role2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config =>
                {
                    config.LoginPath = "/Admin/Login";
                    config.AccessDeniedPath = "/Home/AccessDenied";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorRead", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                options.AddPolicy("ManagerRead", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Manager");   
                });

                options.AddPolicy("CustomerRead", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Customer");
                });

                options.AddPolicy("SellerRead", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
                                                    || x.User.HasClaim(ClaimTypes.Role, "Seller"));
                    //builder.RequireClaim(ClaimTypes.Role, "Seller");
                    //builder.RequireRole(
                    //    "Manager",
                    //    "Seller");
                });
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
