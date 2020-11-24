using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace Configuration.Web.ApplicationInsights
{
    public class CloudRoleTelemetryInitializer : ITelemetryInitializer
    {
        private readonly ApplicationInsightsSettings settings;
        private static readonly string MachineName = Environment.MachineName.ToLower();

        public CloudRoleTelemetryInitializer(ApplicationInsightsSettings settings)
        {
            this.settings = settings;
        }

        public void Initialize(ITelemetry telemetry)
        {
            // Set custom role name here
            telemetry.Context.Cloud.RoleName = settings.CloudRoleName;

            // Set custom role instance here
            telemetry.Context.Cloud.RoleInstance = MachineName;
        }
    }
}
