using System;
using Postogram.Common;
using Postogram.Common.Logger;
using Serilog;

namespace Postogram.Server.Logger
{
    public class SerilogAdapter : Common.Logger.ILogger
    {
        private readonly FileHelper _fileHelper;

        public ILogWriter GlobalWriter { get; }

        public SerilogAdapter(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            GlobalWriter = CreateSerilogLogger(null);
        }

        public ILogWriter CreateWriter<T>()
        {
            return CreateSerilogLogger(typeof(T));
        }

        private SerilogLogWriter CreateSerilogLogger(Type source)
        {
            var logFilesOutp = _fileHelper.GetFile(Location.Log, "log-.txt");
            Serilog.ILogger seriLogger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(logFilesOutp, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            seriLogger = source == null ? seriLogger : seriLogger.ForContext(source);

            return new SerilogLogWriter(seriLogger);
        }
    }
}
