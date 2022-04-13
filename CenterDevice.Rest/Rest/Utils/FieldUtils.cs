using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CenterDevice.Rest.Utils
{
    static class FieldUtils
    {
        public static string GetFieldIncludes(Type type)
        {
            return string.Join(",", GetAllFields(type));
        }

        public static IEnumerable<string> GetAllFields(Type clazz)
        {
            foreach (var property in clazz.GetProperties())
            {
                if (property.CanWrite)
                {
                    var attribute = (JsonPropertyNameAttribute)Attribute.GetCustomAttribute(property, typeof(JsonPropertyNameAttribute));
                    if (attribute != null)
                    {
                        yield return attribute.Name;
                    }
                    else
                    {
                        yield return property.Name.ToLower();
                    }
                }
            }
        }
    }
}
