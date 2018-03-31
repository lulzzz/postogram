using System;
using System.Runtime.CompilerServices;
using Postogram.Common.Logger;

namespace Postogram.Server.Logger
{
    public class SerilogLogWriter : ILogWriter
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogWriter(Serilog.ILogger serilogLogger)
        {
            _logger = serilogLogger;
        }

        public void Debug(string pattern, params object[] args)
        {
            _logger.Debug(pattern, propertyValue: args);
        }

        public void Error(string pattern, params object[] args)
        {
            _logger.Error(pattern, propertyValue: args);
        }

        public void Info(string pattern, params object[] args)
        {
            _logger.Information(pattern, propertyValue: args);
        }

        public void Warning(string pattern, params object[] args)
        {
            _logger.Warning(pattern, propertyValue: args);
        }


        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }

        public void Debug(object args, [CallerMemberName]string methodCaller = null)
        {
            _logger.Debug(methodCaller, args);
        }
    }
}
