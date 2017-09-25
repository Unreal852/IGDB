using IGDBLib.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace IGDBLib.Helpers
{
    public static class TypeHelper
    {
        public static IGDBFields[] GetPropertiesFields(Type type)
        {
            List<IGDBFields> fields = new List<IGDBFields>();
            foreach (PropertyInfo info in type.GetPropertiesByAttribute(typeof(IGDBValue)))
                fields.Add(info.GetCustomAttribute<IGDBValue>().Field);
            return fields.ToArray();
        }
    }
}
