using System;

namespace Build1.PostMVC.Utils.Extensions
{
    public static class TypeExtension
    {
        public static object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}