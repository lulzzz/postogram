using System;

namespace Postogram.Common.Configuration
{
    public interface IConfigurationReader
    {
        T Read<T>(string name);
        T Read<T>(string name, T defaultValue);
        object Read(string name, Type type);
        object Read(string name, object defaultValue, Type type);
    }
}
