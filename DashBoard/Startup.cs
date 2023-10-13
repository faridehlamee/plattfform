using Autofac;
using Common;
using Data.CustomMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFrameWork.Configuration;
using WebFrameWork.Middlewares;

namespace DashBoard
{
    public class Startup
    {
        private readonly SiteSettings _siteSettings;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.InitializeAutoMapper();
            services.AddDbContext(Configuration);
            services.AddMvc(setupAction => {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();
            services.AddCustomIdentity(_siteSettings.IdentitySettings);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Register Services to Autofac ContainerBuilder
            builder.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCustomExceptionHandler();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllers();
                config.MapAreaControllerRoute(
                    name: "areas",
                    "areas",
                    pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");

                config.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
