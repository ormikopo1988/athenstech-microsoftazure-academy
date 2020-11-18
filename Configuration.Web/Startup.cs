using Configuration.Web.DbAccess;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Configuration.Web.Repositories;
using Configuration.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Configuration.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // For use in ReadUsingOptionsPattern page
            services.Configure<PositionOptions>(
                Configuration.GetSection(PositionOptions.Position)
            );

            // Register the settings for "OAuthSettings" section
            var settings = new OAuthSettings();
            Configuration.Bind(OAuthSettings.OAuthSection, settings);
            services.AddSingleton(settings);

            // ProjectService Registration
            services.AddScoped<IProjectService, ProjectService>();

            // ProjectRepository Registration
            services.AddScoped<IProjectRepository, ProjectRepository>();

            // Register the SchoolContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
