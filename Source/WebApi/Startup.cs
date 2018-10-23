﻿using Drm.Data;
using Drm.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drm.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DRMContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DRMConnectionString"), opt => opt.MigrationsAssembly(typeof(Startup).Namespace));
            });

            services.AddIdentity<DrmUser, DrmRole>()
                        .AddEntityFrameworkStores<DRMContext>();

            services.AddTransient<DRMSeeder>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
//            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
            "/{controller}/{action}/{id?}",
            new { Controller = "App", Action = "Index" });
            });


        }
    }
}
