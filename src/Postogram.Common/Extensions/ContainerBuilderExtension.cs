using Autofac;
using Postogram.Common.Configuration;

namespace Postogram.Common
{
    public static class ContainerBuilderExtension
    {
        public static void RegisterConfigurationSection<TConfigSection>(this ContainerBuilder builder)
            where TConfigSection : IConfigurationSection, new()
        {
            builder.Register(CreateConfigInstance);

            TConfigSection CreateConfigInstance(IComponentContext context)
            {
                var configurationInstance = context.Resolve<IConfiguration>();
                return configurationInstance.GetConfigSection<TConfigSection>();
            }
        }
    }
}
