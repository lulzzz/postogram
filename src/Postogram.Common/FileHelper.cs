using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Postogram.Common
{
    public static class FileHelper
    {
        public static DirectoryInfo GetDirectory(Location location, string subDirectory = null)
        {
            throw new NotImplementedException();
        }

        public static string GetFile(Location location, string subDirectory, string file)
        {
            var dir = GetDirectory(location, subDirectory);
            return Path.Combine(dir.FullName, file);
        }

        public static string GetFile(Location location, string file) =>
            GetFile(location, null, file);
    }
}
