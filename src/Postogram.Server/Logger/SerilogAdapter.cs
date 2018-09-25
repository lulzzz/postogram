using System;
using Serilog;
using Postogram.Common;
using Postogram.Common.Logger;
using ILogger = Postogram.Common.Logger.ILogger;

namespace Postogram.Server.Logger
{
    public class SerilogAdapter : ILogger
    {
        private readonly IFilePathHelper _fileHelper;
        public ILogWriter GlobalWriter { get; }

        public SerilogAdapter(IFilePathHelper filePathHelper)
        {
            _fileHelper = filePathHelper;
            GlobalWriter = CreateSerilogLogger(null);
        }

        public ILogWriter CreateWriter<T>() => CreateSerilogLogger(typeof(T));

        private SerilogLogWriter CreateSerilogLogger(Type source)
        {
            var logFilePath = _fileHelper.GetFile(Location.Log, "log-.txt");

            var logger = (Serilog.ILogger) new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            if (source != null)
            {
                logger = logger.ForContext(source);
            }

            return new SerilogLogWriter(logger);
        }
    }
}
