
namespace Postogram.Common.Configuration
{
    public interface IConfigurationSection
    {
        void Init(IConfigurationReader reader);
    }
}
