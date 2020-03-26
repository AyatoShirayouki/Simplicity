using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplicity.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T type)
        {
            if (type == null) { throw new ArgumentNullException(nameof(type)); }

            FieldInfo fi = type.GetType().GetField(type.ToString());

            var attributes = fi == null ? null :
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return type.ToString();
        }
    }
}
