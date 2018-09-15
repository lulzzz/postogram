using Postogram.Common.Configuration.BaseConfigurationAnnotations;

namespace Postogram.Common.Configuration
{
    public class ApplicationEnviromentConfiguration : BaseConfigurationSection
    {
        [Required]
        public string ApplicationName { get; set; }
    }
}
