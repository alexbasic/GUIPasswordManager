using Ru.Mail.AlexBasic.GUIPasswordManager.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db
{
    internal static class SqliteTools
    {
        public static T SqliteAsType<T>(this object value)
        {
            if (value == null)
                return default(T);
            return (value.GetType() == typeof(long)) ? 
                value.AsType<T>() :
                (T)value; 
        }

        public static object SqliteAsType(this object value, Type destinationType = null)
        {
            if (value == null) 
                return value;
            if (value.GetType() == typeof(long) && destinationType != typeof(bool))
                return value.AsType<int>();
            if (destinationType != null && destinationType == typeof(bool))
            {
                var semiResult = value.AsType<int>();
                object result = semiResult > 0 ? true : false;
                return result;
            }
            return value;
        }
    }
}
