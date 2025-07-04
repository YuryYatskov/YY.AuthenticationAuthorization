using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace JWTBearer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    byte[] secretKey = Encoding.UTF8.GetBytes(Constans.SecretKey);
                    var key = new SymmetricSecurityKey(secretKey);

                    config.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("t"))
                            {
                                context.Token = context.Request.Query["t"];
                            }
                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters
                    { 
                        ValidIssuer = Constans.Issuer,
                        ValidAudience = Constans.Audience,
                        IssuerSigningKey = key
                    };
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
