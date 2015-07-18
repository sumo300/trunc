using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;

namespace Trunc.Extensions {
    public static class AttributeExtensions {
        private static readonly ResourceManager _resource;

        public static string GetDisplayValue(this Enum enumValue) {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = (DisplayAttribute[]) fi.GetCustomAttributes(typeof (DisplayAttribute), false);

            if (attributes.Length <= 0) return enumValue.ToString();
            string resourceValue = _resource.GetString(attributes[0].Name);
            return string.IsNullOrWhiteSpace(resourceValue) ? enumValue.ToString() : resourceValue;
        }
    }
}