using Postogram.Common.Configuration.BaseConfigurationAnnotations;

namespace Postogram.Common.Configuration
{
    public class CustomPathsConfiguration : BaseConfigurationSection
    {
        [Default(DefaultValue ="Logs")]
        public string LogDirectoryName { get; set; }
        public string CustomLogFilesPath { get; set; }
        public string CustomTemporaryFilesPath { get; set; }
        public string CustomApplicationFilesPath { get; set; }
    }
}
