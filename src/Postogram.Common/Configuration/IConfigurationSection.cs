using System;
using System.Collections.Generic;
using System.Text;

namespace Postogram.Common.Configuration
{
    public interface IConfigurationSection
    {
        void Init(IConfigurationReader reader);
    }
}
