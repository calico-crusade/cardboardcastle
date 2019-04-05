using CardboardBox.Redis;
using CardboardBox.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace CardboardCastle
{
    using SqlServer;

    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var fp = new PhysicalFileProvider(AppContext.BaseDirectory);

            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(fp, "appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            var redisSettings = Configuration.GetSection("Redis").Get<CommonConfig>();
            var databSettings = Configuration.GetSection("Database").Get<DatabaseConfig>();

            return services
                .Jwt(Configuration["Tokens:Issuer"], Configuration["Tokens:Key"])
                .Map(_ =>
                {
                    _.Use<ICommonConfig>(redisSettings);
                    _.Use<IDatabaseConfig>(databSettings);
                    _.AddNLog();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            //Development environment settings
            if (env.IsDevelopment())
            {
                //Developer exception page to display exception messages
                app.UseDeveloperExceptionPage();
                //Set caching options to invalid static files every 600 milliseconds
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=600");
                    }
                });
            }
            else
            {
                //redirect to angular error page
                app.UseExceptionHandler("/error");
                //Allow the use of static files
                app.UseStaticFiles();
            }

            app.UseMvcWithDefaultRoute();
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), b =>
            {
                b.UseMvc(routes =>
                {
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
                });
            });
        }
    }
}
