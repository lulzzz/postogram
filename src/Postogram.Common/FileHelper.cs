using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using static System.Environment;

namespace Postogram.Common
{
    public static class FileHelper
    {
        private const string AppName = "Postogram";
        private const string LogDirectory = "Logs";

        public static DirectoryInfo GetDirectory(Location location, string subDirectory = null)
        {
            string basePath;
            switch (location)
            {
                case Location.Application:
                    basePath = Path.Combine(
                        Environment.GetFolderPath(SpecialFolder.CommonApplicationData),
                        AppName);
                    break;
                case Location.Log:
                    basePath = GetDirectory(Location.Application, LogDirectory).FullName;
                    break;
                case Location.Temporary:
                    basePath = Path.Combine(Path.GetTempPath(), AppName);
                    break;
                default:
                    throw new NotImplementedException();
            }
            var path = Path.Combine(basePath, (subDirectory ?? String.Empty));
            return EnsureDirectoryExists(path);
        }

        public static string GetFile(Location location, string subDirectory, string file)
        {
            var dir = GetDirectory(location, subDirectory);
            return Path.Combine(dir.FullName, file);
        }

        public static string GetFile(Location location, string file) =>
            GetFile(location, null, file);


        private static DirectoryInfo EnsureDirectoryExists(string path)
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
