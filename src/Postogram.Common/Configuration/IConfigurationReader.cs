using System;
using System.Collections.Generic;
using System.Text;

namespace Postogram.Common.Configuration
{
    public interface IConfigurationReader
    {
        T Read<T>(string name);
        T Read<T>(string name, T defaultValue);
    }
}
