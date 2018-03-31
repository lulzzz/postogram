using System;
using System.Collections.Generic;
using System.Text;

namespace Postogram.Common.Logger
{
    public interface ILogger
    {
        ILogWriter GlobalWriter { get; }
        ILogWriter CreateWriter<TContext>();
    }
}
