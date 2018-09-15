using System;

namespace Postogram.Common.Configuration.BaseConfigurationAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : Attribute
    {
    }
}
