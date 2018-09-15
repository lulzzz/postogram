using System;
using System.Configuration;
using System.Collections.Specialized;
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
                    .None(() => throw CreateConfigNotFoundException(name, _sectionName));
        }

        public T Read<T>(string name, T defaultValue)
        {
            return ReadOption<T>(name)
                    .Some(value => value)
                    .None(() => defaultValue);
        }

        public object Read(string name, Type type)
        {
            return ReadWithType(name, type)
                ?? throw CreateConfigNotFoundException(name, _sectionName);
        }

        public object Read(string name, object defaultValue, Type type)
        {
            return ReadWithType(name, type) ??
                Convert.ChangeType(defaultValue, type);
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

        private object ReadWithType(string name, Type type)
        {
            try
            {
                var section = ConfigurationManager.GetSection(_sectionName) as NameValueCollection;
                var record = section[name];
                return Convert.ChangeType(record, type);
            }
            catch
            {
                return null;
            }
        }

        private static Exception CreateConfigNotFoundException(string paramName, string sectionName)
            => new ArgumentException($"Parameter '{paramName}' not found in section '{sectionName}'");
    }
}
