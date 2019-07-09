using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FeedbackWebsite.Utils
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";

            var displayName = DisplayNameCache.GetOrAdd(key, x =>
            {
                var name = (DisplayNameAttribute[]) value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DisplayNameAttribute), false);

                return name.Length > 0 ? name[0].DisplayName : value.ToString();
            });

            return displayName;
        }
    }
}
