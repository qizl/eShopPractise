using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace EnjoyCodes.eShopOnWeb.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            this._logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            this._logger.LogInformation(message, args);
        }
    }
}
