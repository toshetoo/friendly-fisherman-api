using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FriendlyFisherman.SharedKernel.Helpers
{
    public class Mapper<TR, TE> where TR : class, new()
    {
        public static TR Map(TE item)
        {
            var result = new TR();
            var props = result.GetType().GetProperties();

            for (int j = 0; j < props.Length; j++)
            {
                if (item.GetType().GetProperty(props[j].Name) != null)
                {
                    var newValue = item.GetType().GetProperty(props[j].Name).GetValue(item);
                    if (newValue != null)
                    {
                        var propInfo = result.GetType().GetProperty(props[j].Name);
                        propInfo.SetValue(result, ChangeType(newValue, propInfo.PropertyType));
                    }
                }
            }

            return result;
        }

        public static IEnumerable<TR> MapList(List<TE> items)
        {
            var result = new List<TR>();
            var props = new TR().GetType().GetProperties();
            for (int i = 0; i < items.Count(); i++)
            {
                var el = items[i];
                var propEl = new TR();

                for (int j = 0; j < props.Length; j++)
                {
                    if (el.GetType().GetProperty(props[j].Name) != null)
                    {
                        var newValue = el.GetType().GetProperty(props[j].Name).GetValue(el);
                        if (newValue != null)
                        {
                            var propInfo = propEl.GetType().GetProperty(props[j].Name);
                            propInfo.SetValue(propEl, ChangeType(newValue, propInfo.PropertyType));
                        }

                    }
                }

                result.Add(propEl);
            }

            return result;
        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }
    }

}
