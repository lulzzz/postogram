using System;
using System.Collections.Specialized;
using System.Configuration;
using LanguageExt;
using Postogram.Common.Configuration;

namespace Postogram.Server.Configuration
{
    public class AppSettingsConfigurationReader : IConfigurationReader
    {
        private readonly string _sectionName;

        public AppSettingsConfigurationReader(string sectionName)
        {
            _sectionName = sectionName;
        }

        public T Read<T>(string name)
        {
            return ReadOption<T>(name)
                    .Some(value => value)
                    .None(() => throw new ArgumentException($"Parameter '{name}' not found in section '{_sectionName}'"));
        }

        public T Read<T>(string name, T defaultValue)
        {
            return ReadOption<T>(name)
                    .Some(value => value)
                    .None(() => defaultValue);
        }

        private Option<T> ReadOption<T>(string name)
        {
            try
            {
                var section = ConfigurationManager.GetSection(_sectionName) as NameValueCollection;
                var record = section[name];
                return (T)Convert.ChangeType(record, typeof(T));
            }
            catch
            {
                return Option<T>.None;
            }
        }
    }
}
