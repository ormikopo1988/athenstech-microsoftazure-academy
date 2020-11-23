using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logging.Web.Models;
using System;

namespace Logging.Web.Controllers
{
    public class HomeController : Controller
    {
        // This <HomeController> is called a Log category
        // and it allows us to categorize our different
        // messages inside our logging system.
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // If we want to define a custom category for our log messages
        //private readonly ILogger _logger;

        //public HomeController(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger("CustomCategoryName");
        //}

        public IActionResult Index()
        {
            // This will output something like the following:
            // info: Logging.Web.Controllers.HomeController[0] <- This [0] is the default event id
            //       Information log message.
            _logger.LogInformation("Information log message.");

            // If we want, we can associate our custom event ids with specific events, like so:
            _logger.LogInformation(LoggingEventId.DemoCode, "Information log message with custom event id.");

            // Now let's examine the different log levels supported (from least significant to most):
            // - Trace: Typically used for logs that will have a really detailed view in what exactly is going on (may also include some application secrets inside them)
            // - Debug: Pretty much like trace logs used for logging heavy debugging information
            // - Information: More like a flow of how our application is being used. Information level logs will probably be kept for our application
            // - Warning: Used to log some issue in your code (maybe log an exception that you catch inside a try-catch block)
            // - Error: Used to log some more important issue in your application. Maybe some part of your app is failing.
            // - Critical: Used to log some critical issue regarding your app. Maybe major parts of your app are crashing and the app is not doing what it is supposed to do.
            _logger.LogTrace("Trace log.");
            _logger.LogDebug("Debug log.");
            _logger.LogInformation("Information log.");
            _logger.LogWarning("Warning log.");
            _logger.LogError("Error log.");
            _logger.LogCritical("Critical log.");

            // We can also pass data in the messages we log, like so:
            _logger.LogError("There was some kind of error at {Time}", DateTime.UtcNow);

            try
            {
                throw new Exception("Some mock exception happened here.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "There was a critical error at {Time}", DateTime.UtcNow);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class LoggingEventId
    {
        public const int DemoCode = 1903;
    }
}
