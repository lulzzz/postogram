using Postogram.Common.Configuration;
using Postogram.Common.Configuration.BaseConfigurationAnnotations;

namespace Postogram.InstagramClient
{
    public class InstagramConfiguration : BaseConfigurationSection
    {
        [Default(DefaultValue = "false")]
        public bool ToLogRequests { get; set; }
    }
}
