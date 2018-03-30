using System;
using Postogram.Common.Configuration;

namespace Postogram.Common.Configuration
{
    public interface IConfiguration
    {
        TConfigSection GetConfigSection<TConfigSection>() where TConfigSection : IConfigurationSection, new();
        TConfigSection GetConfigSection<TConfigSection>(string sectionName) where TConfigSection : IConfigurationSection, new();
    }
}
