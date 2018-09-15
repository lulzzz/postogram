using System.IO;

namespace Postogram.Common
{
    public interface IFilePathHelper
    {
        DirectoryInfo GetDirectory(Location location, string subDirectory = null);
        string GetFile(Location location, string file);
        string GetFile(Location location, string subDirectory, string file);
    }
}
