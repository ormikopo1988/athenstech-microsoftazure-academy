namespace Configuration.Web.ApplicationInsights
{
    public class ApplicationInsightsSettings
    {
        public const string ApplicationInsightsSectionKey = "ApplicationInsights";

        public bool DisableTelemetry { get; set; }
        public string CloudRoleName { get; set; }
    }
}
