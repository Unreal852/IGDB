using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IGDBLib.Extenders
{
    public static class JsonExts
    {
        /// <summary>
        /// Get the given value and deserialize it to the given type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">JObject</param>
        /// <param name="value">Value to get from JObject</param>
        /// <returns>Deserialized Value</returns>
        public static T Get<T>(this JObject obj, string value)
        {
            return JsonConvert.DeserializeObject<T>(obj[value].ToString());
        }

        /// <summary>
        /// Get the given value and deserialize it to the given type
        /// </summary>
        /// <param name="obj">JObject</param>
        /// <param name="type">Type</param>
        /// <param name="value">Value to get from JObject</param>
        /// <returns>Deserialized Value</returns>
        public static object Get(this JObject obj, Type type, string value)
        {
            return JsonConvert.DeserializeObject(obj[value].ToString());
        }
        
        /// <summary>
        /// Check if the given token is null or empty
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Is Null or Empty</returns>
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
    }
}
