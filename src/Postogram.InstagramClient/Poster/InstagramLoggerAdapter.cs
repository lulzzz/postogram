using System;
using System.Net.Http;
using InstaSharper.Logger;
using Postogram.Common.Logger;

namespace Postogram.InstagramClient.Poster
{
    public class InstagramLoggerAdapter : IInstaLogger
    {
        private readonly ILogWriter _logWriter;
        private readonly bool _toLogRequests;

        public InstagramLoggerAdapter(ILogWriter logWriter, bool toLogRequests)
        {
            _logWriter = logWriter;
            _toLogRequests = toLogRequests;
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
            if (_toLogRequests)
            {
                _logWriter.Debug("Request message: {request}", request);
            }
        }

        public void LogRequest(Uri uri)
        {
            if (_toLogRequests)
            {
                _logWriter.Debug("Request uri: {uri}", uri);
            }
        }

        public void LogResponse(HttpResponseMessage response)
        {
            if (_toLogRequests)
            {
                _logWriter.Debug("Response message: {uri}", response);
            }
        }
    }
}
