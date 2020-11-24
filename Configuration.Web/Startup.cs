using Configuration.Web.ApplicationInsights;
using Configuration.Web.DbAccess;
using Configuration.Web.Interfaces;
using Configuration.Web.Models;
using Configuration.Web.Repositories;
using Configuration.Web.Services;
using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

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

            // Register the ApplicationDbContext
            // Create the database by following steps that use migrations to create it:
            // - dotnet tool install --global dotnet-ef
            // - dotnet add package Microsoft.EntityFrameworkCore.Design
            // - dotnet ef migrations add InitialCreate
            // - dotnet ef database update
            // This installs dotnet ef and the design package which is required to run the command on a project. 
            // The migrations command scaffolds a migration to create the initial set of tables for the model. 
            // The database update command creates the database and applies the new migration to it.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            // Register the settings for "ApplicationInsights" section as a service for injection from DI container
            var applicationInsightsSettings = new ApplicationInsightsSettings();
            Configuration.Bind(ApplicationInsightsSettings.ApplicationInsightsSectionKey, applicationInsightsSettings);
            services.AddSingleton(applicationInsightsSettings);

            // Use telemetry initializers when you want to enrich telemetry with additional information
            services.AddSingleton<ITelemetryInitializer, CloudRoleTelemetryInitializer>();

            // Remove a specific built-in telemetry initializer
            var telemetryInitializerToRemove = services.FirstOrDefault<ServiceDescriptor>
                                (t => t.ImplementationType == typeof(AspNetCoreEnvironmentTelemetryInitializer));

            if (telemetryInitializerToRemove != null)
            {
                services.Remove(telemetryInitializerToRemove);
            }

            // You can add custom telemetry processors to TelemetryConfiguration by using the extension method AddApplicationInsightsTelemetryProcessor on IServiceCollection. 
            // You use telemetry processors in advanced filtering scenarios
            services.AddApplicationInsightsTelemetryProcessor<StaticWebAssetsTelemetryProcessor>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TelemetryConfiguration configuration, ApplicationInsightsSettings applicationInsightsSettings)
        {
            configuration.DisableTelemetry = applicationInsightsSettings.DisableTelemetry;

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
