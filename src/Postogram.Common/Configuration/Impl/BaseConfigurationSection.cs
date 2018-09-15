using System.Reflection;
using Postogram.Common.Configuration.BaseConfigurationAnnotations;

namespace Postogram.Common.Configuration
{
    public class BaseConfigurationSection : IConfigurationSection
    {
        public virtual void Init(IConfigurationReader reader)
        {
            foreach(var property in GetProperties())
            {
                object value;
                var defaultValue = GetDefaultValue(property);

                if (IsRequired(property) && defaultValue == default)
                {
                    value = reader.Read(property.Name, property.PropertyType);
                }
                else
                {
                    value = reader.Read(property.Name, defaultValue, property.PropertyType);
                }
                
                property.SetValue(this, value);
            }
        }

        protected virtual void Validate()
        {
        }

        private PropertyInfo[] GetProperties()
        {
            var type = GetType();
            return type.GetProperties();
        }

        private bool IsRequired(PropertyInfo property)
        {
            return property.GetCustomAttribute<RequiredAttribute>() != null;
        }

        private object GetDefaultValue(PropertyInfo property)
        {
            return property.GetCustomAttribute<DefaultAttribute>()?.DefaultValue;
        }
    }
}
