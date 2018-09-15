using System;
using Serilog;
using Postogram.Common;
using Postogram.Common.Logger;

namespace Postogram.Server.Logger
{
    public class SerilogAdapter : Common.Logger.ILogger
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
            var logFilesOutp = _fileHelper.GetFile(Location.Log, "log-.txt");

            var logger = (Serilog.ILogger) new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(logFilesOutp, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            logger = source == null
                ? logger
                : logger.ForContext(source);

            return new SerilogLogWriter(logger);
        }
    }
}
