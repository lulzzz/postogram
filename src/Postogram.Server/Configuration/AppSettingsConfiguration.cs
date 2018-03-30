using System.Collections.Generic;
using Postogram.Common.Configuration;

namespace Postogram.Server.Configuration
{
    public class AppSettingsConfiguration : IConfiguration
    {
        private readonly Dictionary<string, IConfigurationSection> _cached;

        public AppSettingsConfiguration()
        {
            _cached = new Dictionary<string, IConfigurationSection>();
        }

        public T GetConfigSection<T>() where T : IConfigurationSection, new()
        {
            return GetConfigSection<T>(typeof(T).Name);
        }

        public T GetConfigSection<T>(string sectionName) where T : IConfigurationSection, new()
        {
            if(_cached.ContainsKey(sectionName))
            {
                return (T)_cached[sectionName];
            }

            var section = new T();
            section.Init(new AppSettingsConfigurationReader(sectionName));

            _cached[sectionName] = section;
            return section;
        }
    }
}
