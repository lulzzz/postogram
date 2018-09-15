using System;
using System.IO;
using static System.Environment;
using static System.IO.Path;

namespace Postogram.Common
{
    public class FilePathHelper : IFilePathHelper
    {
        private readonly ApplicationEnviroment _configuration;

        public FilePathHelper(ApplicationEnviroment configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DirectoryInfo GetDirectory(Location location, string subDirectory = null)
        {
            string basePath;
            switch (location)
            {
                //within custom paths
                case Location.Log when HasCustomPath(_configuration.CustomLogFilesPath):
                    basePath = _configuration.CustomLogFilesPath;
                    break;
                case Location.Temporary when HasCustomPath(_configuration.CustomTemporaryFilesPath):
                    basePath = _configuration.CustomTemporaryFilesPath;
                    break;
                case Location.Application when HasCustomPath(_configuration.CustomApplicationFilesPath):
                    basePath = _configuration.CustomApplicationFilesPath;
                    break;

                //without custom pathes
                case Location.Application:
                    var commonAppData = GetFolderPath(SpecialFolder.CommonApplicationData);
                    basePath = Combine(commonAppData, _configuration.ApplicationName);
                    break;
                case Location.Log:
                    basePath = GetDirectory(Location.Application, _configuration.LogDirectoryName).FullName;
                    break;
                case Location.Temporary:
                    basePath = Combine(GetTempPath(), _configuration.ApplicationName);
                    break;
                default:
                    throw new NotImplementedException();
            }
            var path = Combine(basePath, (subDirectory ?? String.Empty));
            return EnsureDirectoryExists(path);

            bool HasCustomPath(string customPath) => !String.IsNullOrEmpty(customPath);
        }

        public string GetFile(Location location, string file) => GetFile(location, null, file);
        public string GetFile(Location location, string subDirectory, string file)
        {
            if(String.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file));
            }

            var dir = GetDirectory(location, subDirectory);
            return Combine(dir.FullName, file);
        }

        private DirectoryInfo EnsureDirectoryExists(string path)
        {
            var di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }
    }
}
