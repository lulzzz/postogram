using System;

namespace Postogram.Common.Configuration.BaseConfigurationAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =false)]
    public class DefaultAttribute : Attribute
    {
        public string DefaultValue { get; set; }
    }
}
