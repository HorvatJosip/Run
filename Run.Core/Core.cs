using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Run.Core
{
    public static class Core
    {
        /// <summary>
        /// Provider for the collection of services for this project.
        /// </summary>
        public static IServiceProvider Provider { get; private set; }

        /// <summary>
        /// Configuration for this project.
        /// </summary>
        public static IConfigurationService Configuration => Get<IConfigurationService>();

        /// <summary>
        /// Logger used for this project.
        /// </summary>
        public static ILogger Logger => Get<ILogger>();

        public static void Setup(Action<ServiceCollection> addServices)
        {
            // Create service collection
            var services = new ServiceCollection();

            // Allow adding logging implementations
            services.AddLogging(loggingBuilder =>
            {
                // Add debug logger
                loggingBuilder.AddDebug();
            });

            // Allow the insertion of services
            addServices?.Invoke(services);

            // Build the provider
            Provider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets a service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to get.</typeparam>
        public static T Get<T>() => Provider.GetService<T>();

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void Log(string message)
        {
            if (Logger != null)
                Logger.Log(LogLevel.Information, message);
            else
                Debug.WriteLine($"Info: {message}");
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Exception to log.</param>
        public static void LogError(string message, Exception exception = null)
        {
            if (Logger != null)
                Logger.Log(LogLevel.Error, exception, message);
            else
            {
                Debug.WriteLine($"Error: {message}");

                if (exception != null)
                    Debug.WriteLine(exception);
            }
        }
    }
}
