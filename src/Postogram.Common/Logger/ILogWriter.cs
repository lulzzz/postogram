using System;
using System.Collections.Generic;
using System.Text;

namespace Postogram.Common.Logger
{
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
