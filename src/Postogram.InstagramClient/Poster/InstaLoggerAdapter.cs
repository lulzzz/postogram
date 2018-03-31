using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InstaSharper.Logger;
using Postogram.Common.Logger;

namespace Postogram.InstagramClient.Poster
{
    public class InstaLoggerAdapter : IInstaLogger
    {
        private readonly ILogWriter _logWriter;

        public InstaLoggerAdapter(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void LogException(Exception exception)
        {
            _logWriter.Error(exception, String.Empty);
        }

        public void LogInfo(string info)
        {
            _logWriter.Info(info);
        }

        public void LogRequest(HttpRequestMessage request)
        {
            // no need
        }

        public void LogRequest(Uri uri)
        {
            // no need
        }

        public void LogResponse(HttpResponseMessage response)
        {
            // no need
        }
    }
}
