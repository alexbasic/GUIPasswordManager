using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Tools
{
    public static class ObjectExtensions
    {
        public static T AsType<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static void SetValue(this object obj, string fieldName, object value)
        {
            obj?.GetType()?.GetProperty(fieldName)?.SetValue(obj, value);
        }

        public static IEnumerable<KeyValuePair<string, object>> GetPropertiesValues(this object obj)
        {
            var propInfos = obj?.GetType()?.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propInfo in propInfos)
            {
                var k = propInfo.Name;
                var v = propInfo.GetValue(obj);
                yield return new KeyValuePair<string, object>(k, v);
            }
        }
    }
}
