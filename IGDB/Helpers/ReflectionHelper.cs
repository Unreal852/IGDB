using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IGDBLib.Helpers
{
    public static class ReflectionHelper
    {
        private static readonly Type[] PrimitiveType = new Type[]
            {
                typeof(Enum),
                typeof(String),
                typeof(Decimal),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(Guid),
                typeof(int)
            };

        /// <summary>
        /// Check if the given type is a list
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>TRUE if the type is a list otherwise FALSE</returns>
        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        /// <summary>
        /// Get all properties by the given attribute type
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="attribute">Attribute Type</param>
        /// <returns>PropertyInfo</returns>
        public static List<PropertyInfo> GetPropertiesByAttribute(this Type type, Type attribute)
        {
            return type.GetProperties().Where(prop => Attribute.IsDefined(prop, attribute)).ToList();
        }

        /// <summary>
        /// Check if the given type is primitive
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>TRUE if primitive otherwise FALSE</returns>
        public static bool IsPrimitiveType(this Type type)
        {
            return type.IsPrimitive || PrimitiveType.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsPrimitiveType(type.GetGenericArguments()[0]));
        }
    }
}
