using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using static System.Environment;

namespace Postogram.Common
{
    public class FileHelper
    {
        private const string AppName = "Postogram";
        private const string LogDirectory = "Logs";

        public DirectoryInfo GetDirectory(Location location, string subDirectory = null)
        {
            string basePath;
            switch (location)
            {
                case Location.Application:
                    var commonAppData = GetFolderPath(SpecialFolder.CommonApplicationData);
                    basePath = Path.Combine(commonAppData, AppName);
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

        public string GetFile(Location location, string subDirectory, string file)
        {
            if(String.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file));
            }

            var dir = GetDirectory(location, subDirectory);
            return Path.Combine(dir.FullName, file);
        }

        public string GetFile(Location location, string file) =>
            GetFile(location, null, file);


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
