using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postogram.Common.Configuration;

namespace Postogram.InstagramClient
{
    public class InstagramConfiguration : IConfigurationSection
    {
        public void Init(IConfigurationReader reader)
        {
            Login = reader.Read<string>(nameof(Login));
            Password = reader.Read<string>(nameof(Password));
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
    }
}
