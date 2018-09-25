using System;
using System.IO;
using static System.IO.Path;
using static System.Environment;
using Postogram.Common.Configuration;

namespace Postogram.Common
{
    public class FilePathHelper : IFilePathHelper
    {
        private readonly CustomPathsConfiguration _pathsConfig;
        private readonly ApplicationEnviromentConfiguration _environmentConfig;

        public FilePathHelper(ApplicationEnviromentConfiguration environmentConfig,
            CustomPathsConfiguration customPathsConfiguration)
        {
            _environmentConfig = environmentConfig ?? throw new ArgumentNullException(nameof(environmentConfig));
            _pathsConfig = customPathsConfiguration ?? throw new ArgumentNullException(nameof(customPathsConfiguration));
        }

        public DirectoryInfo GetDirectory(Location location, string subDirectory = null)
        {
            string basePath;
            switch (location)
            {
                //within custom paths
                case Location.Log when HasCustomPath(_pathsConfig.CustomLogFilesPath):
                    basePath = _pathsConfig.CustomLogFilesPath;
                    break;
                case Location.Temporary when HasCustomPath(_pathsConfig.CustomTemporaryFilesPath):
                    basePath = _pathsConfig.CustomTemporaryFilesPath;
                    break;
                case Location.Application when HasCustomPath(_pathsConfig.CustomApplicationFilesPath):
                    basePath = _pathsConfig.CustomApplicationFilesPath;
                    break;

                //without custom paths
                case Location.Application:
                    var commonAppData = GetFolderPath(SpecialFolder.CommonApplicationData);
                    basePath = Combine(commonAppData, _environmentConfig.ApplicationName);
                    break;
                case Location.Log:
                    basePath = GetDirectory(Location.Application, _pathsConfig.LogDirectoryName).FullName;
                    break;
                case Location.Temporary:
                    basePath = Combine(GetTempPath(), _environmentConfig.ApplicationName);
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
