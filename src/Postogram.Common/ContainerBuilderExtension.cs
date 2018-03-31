using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Postogram.Common.Configuration;

namespace Postogram.Common
{
    public static class ContainerBuilderExtension
    {
        public static void RegisterConfigurationSection<T>(this ContainerBuilder builder) where T : IConfigurationSection, new()
        {
            T CreateConfigInstance(IComponentContext context)
            {
                var configurationInstance = context.Resolve<IConfiguration>();
                return configurationInstance.GetConfigSection<T>();
            }

            builder.Register(CreateConfigInstance);
        }
    }
}
