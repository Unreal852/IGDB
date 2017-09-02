using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private static Dictionary<string, object> ToDictionary(this JArray array)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();
            foreach (JToken token in array)
            {
                JArray ja = token as JArray;
                if (ja != null)
                    tmp.Add(ToDictionary(ja));
                tmp.Add(token.Path, token);
            }
            return tmp;
        }
    }
}
