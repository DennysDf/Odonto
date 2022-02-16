using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odonto.Services.Implementation;
using Odonto.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Odonto.Models.BDODONTO.Context;

namespace Odonto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DBOdontoContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("ConnectionOdonto")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, User>();
            services.AddSingleton<INotificacao, Notificacao>();
            services.AddTransient<IUpload, Upload>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/";
                    options.LogoutPath = "/Home/Index";
                    options.AccessDeniedPath = "/Home/Logout";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireRole("1"));
                //options.AddPolicy("Orientador", p => p.RequireRole("2"));
                //options.AddPolicy("Alunos", p => p.RequireRole("3"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
