using System;
using Postogram.Common.Configuration;

namespace Postogram.Common
{
    public class ApplicationEnviroment : IConfigurationSection
    {
        public void Init(IConfigurationReader reader)
        {
            throw new NotImplementedException();   
        }

        public string ApplicationName { get; set; }

        public string LogDirectoryName { get; set; }
        public string CustomLogFilesPath { get; set; }
        public string CustomTemporaryFilesPath { get; set; }
        public string CustomApplicationFilesPath { get; set; }
    }
}
