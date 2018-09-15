using System;
using System.Collections.Generic;
using System.Text;

namespace Postogram.Common.Logger
{
    /// <summary>
    /// Structured log writer
    /// </summary>
    /// <remarks>
    /// Implementations must provide structured logging support e.g. <c>Info("Sample log #{number}", number)</c>
    /// </remarks>
    public interface ILogWriter
    {
        void Debug(object args, string methodCaller = null);

        void Debug(string pattern, params object[] args);
        void Info(string pattern, params object[] args);
        void Warning(string pattern, params object[] args);
        void Error(string pattern, params object[] args);

        void Error(Exception exception, string message);
    }
}
