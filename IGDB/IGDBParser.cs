using IGDBLib.Attributes;
using IGDBLib.Converters;
using IGDBLib.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace IGDBLib
{
    internal static class IGDBParser
    {
        /// <summary>
        /// Parse game Json to <see cref="List{Game}"/>
        /// </summary>
        /// <param name="data">JSON data</param>
        /// <returns>Gales</returns>
        public static List<T> ParseGameData<T>(string data)
        {
            List<JObject> tmpGames = JsonConvert.DeserializeObject<List<JObject>>(data);
            if (tmpGames?.Count > 0)
            {
                var games = new List<T>();
                var gameType = typeof(T);
                var props = gameType.GetPropertiesByAttribute(typeof(IGDBValue));
                foreach (JObject obj in tmpGames)
                {
                    var game = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in props)
                        FillProperty(obj, game, property);
                    games.Add(game);
                }
                return games;
            }
            return null;
        }

        /// <summary>
        /// Fill Property
        /// </summary>
        /// <param name="values">Json Values</param>
        /// <param name="obj">Parent Object</param>
        /// <param name="property">Property</param>
        private static void FillProperty(JObject values, object obj, PropertyInfo property)
        {
            try
            {
                Type propertyType = property.PropertyType;
                Type attributeType = typeof(IGDBValue);
                IGDBValue igdbValue = property.GetCustomAttribute<IGDBValue>();
                if (values == null)
                    return;
                if (propertyType.IsPrimitiveType())
                {
                    property.SetValue(obj, Convert.ChangeType(values[igdbValue.Value] == null ? values.ToString() : values[igdbValue.Value], propertyType));
                }
                else if (propertyType.IsEnum)
                {
                    object igdbv = values[igdbValue.Value];
                    object convertedObject = EnumConverters.Convert(igdbv, propertyType);
                    if (convertedObject != igdbv)
                        property.SetValue(obj, convertedObject);
                }
                else if (propertyType.IsList())
                {
                    
                    Type listType = typeof(List<>);
                    Type listContentType = propertyType.GetGenericArguments()[0];
                    Type constructedListType = listType.MakeGenericType(listContentType);
                    IList instance = (IList)Activator.CreateInstance(constructedListType);
                    if (listContentType.IsPrimitiveType())
                    {
                        foreach (JToken token in values.Values())
                        {
                            if (token is JValue)
                                instance.Add(Convert.ChangeType(token, listContentType));
                        }
                    }
                    else if (listContentType.IsEnum)
                    {
                        foreach (JToken token in values[igdbValue.Value].Values())
                            instance.Add(EnumConverters.Convert(token.ToString(), listContentType));
                    }
                    else
                    {
                        object propertyObject = Activator.CreateInstance(listContentType);
                        Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();
                        foreach (PropertyInfo info in listContentType.GetPropertiesByAttribute(typeof(IGDBValue)))
                            props.Add(info.GetCustomAttribute<IGDBValue>().Value, info);
                        foreach (JToken token in values.Values())
                        {
                            if (token is JValue && props.ContainsKey(token.Path))
                            {
                                PropertyInfo pi = props[token.Path];
                                pi.SetValue(propertyObject, Convert.ChangeType(token, pi.PropertyType));
                                instance.Add(propertyObject);
                            }
                        }
                    }
                    property.SetValue(obj, instance); 
                }
                else
                {
                    object propertyObject = Activator.CreateInstance(propertyType);
                    propertyType.GetPropertiesByAttribute(attributeType).ForEach((PropertyInfo p) => FillProperty(values[igdbValue.Value].ToObject<JObject>(), propertyObject, p));
                    property.SetValue(obj, propertyObject);
                }
            }
            catch (Exception ex)
            {
              //  Console.WriteLine(ex.Message);
              //  Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
