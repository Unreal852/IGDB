using IGDBLib.Attributes;
using IGDBLib.Extenders;
using IGDBLib.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public static List<T> ParseData<T>(string data)
        {
            List<JObject> tmpGames = JsonConvert.DeserializeObject<List<JObject>>(data);
            if (tmpGames?.Count > 0)
            {
                List<T> games = new List<T>();
                Type gameType = typeof(T);
                List<PropertyInfo> props = gameType.GetPropertiesByAttribute(typeof(IGDBValue));
                foreach (JObject obj in tmpGames)
                {
                    T game = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in props)
                        FillProperty(obj, game, property); //NEW PROPERTY FILL
                    games.Add(game);
                }
                return games;
            }
            return null;
        }

        /// <summary>
        /// Fill property
        /// </summary>
        /// <param name="values">values</param>
        /// <param name="obj">obj</param>
        /// <param name="property">property</param>
        private static void FillProperty(JObject values, object obj, PropertyInfo property)
        {
            try
            {
                Type propertyType = property.PropertyType;
                IGDBValue igdbValue = property.GetCustomAttribute<IGDBValue>();
                if (values == null)
                    return;
                if (propertyType.IsPrimitiveType())
                {
                    if (!JsonExts.IsNullOrEmpty(values[igdbValue.Value]))
                        property.SetValue(obj, Convert.ChangeType(values[igdbValue.Value], propertyType));
                    else
                        return;
                }
                else if (propertyType.IsEnum)
                {
                    JToken token = values[igdbValue.Value];
                    int enumIndex = -1;
                    if (int.TryParse(token.ToString(), out enumIndex))
                        property.SetValue(obj, Convert.ChangeType(enumIndex, propertyType));
                }
                else if (propertyType.IsList())
                {
                    Type listType = typeof(List<>);
                    Type listContentType = propertyType.GetGenericArguments()[0];
                    Type constructedListType = listType.MakeGenericType(listContentType);
                    IList instance = (IList)Activator.CreateInstance(constructedListType);
                    FillList(values[igdbValue.Value], instance, listContentType);
                    property.SetValue(obj, instance);
                }
                else if (propertyType.IsArray)
                {
                    Type arrayType = propertyType.GetElementType();
                    Array array = Array.CreateInstance(arrayType, 1);
                    FillArray(values[igdbValue.Value], ref array, arrayType);
                    property.SetValue(obj, Convert.ChangeType(array, propertyType));
                }
                else
                {
                    object propertyObject = Activator.CreateInstance(propertyType);
                    FillObject(values[igdbValue.Value], propertyObject, propertyType);
                    property.SetValue(obj, propertyObject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Fill List
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="list">List</param>
        /// <param name="listContentType">Type</param>
        private static void FillList(JToken token, IList list, Type listContentType)
        {
            if (token == null || token.Type != JTokenType.Array)
                return;
            if (listContentType.IsPrimitiveType())
            {
                foreach (JToken jt in token.Values())
                    list.Add(Convert.ChangeType(jt, listContentType));
            }
            else if (listContentType.IsEnum)
            {
                foreach (JToken jt in token.Values())
                {
                    int enumIndex = -1;
                    if (int.TryParse(jt.ToString(), out enumIndex))
                        list.Add(Enum.ToObject(listContentType, enumIndex));
                }
            }
            else
            {
                JArray array = token.ToObject<JArray>();
                foreach (JToken jt in array)
                {
                    object propertyobject = Activator.CreateInstance(listContentType);
                    FillObject(jt, propertyobject, listContentType);
                    list.Add(propertyobject);
                }
            }
        }

        /// <summary>
        /// Fill a array
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="array">Array</param>
        /// <param name="arrayContentType">Type</param>
        private static void FillArray(JToken token, ref Array array, Type arrayContentType)
        {
            if (token == null || token.Type != JTokenType.Array)
                return;
            int index = 0;
            if (arrayContentType.IsPrimitiveType())
            {
                Console.WriteLine("Primitive " + arrayContentType.ToString());
                array = Array.CreateInstance(arrayContentType, token.Values().Count());
                foreach (JToken jt in token.Values())
                {
                    array.SetValue(Convert.ChangeType(jt, arrayContentType), index);
                    index++;
                }
            }
            else if (arrayContentType.IsEnum)
            {
                array = Array.CreateInstance(arrayContentType, token.Values().Count());
                foreach (JToken jt in token.Values())
                {
                    int enumIndex = -1;
                    if (int.TryParse(jt.ToString(), out enumIndex))
                    {
                        array.SetValue(Enum.ToObject(arrayContentType, enumIndex), index);
                        index++;
                    }
                }
            }
            else
            {
                JArray jarray = token.ToObject<JArray>();
                array = Array.CreateInstance(arrayContentType, jarray.Count());
                foreach (JToken jt in jarray)
                {
                    object propertyobject = Activator.CreateInstance(arrayContentType);
                    FillObject(jt, propertyobject, arrayContentType);
                    array.SetValue(propertyobject, index);
                    index++;
                }
            }
        }

        /// <summary>
        /// Fill object
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="obj">object</param>
        /// <param name="propertyType">Type</param>
        private static void FillObject(JToken token, object obj, Type propertyType)
        {
            List<PropertyInfo> properties = propertyType.GetPropertiesByAttribute(typeof(IGDBValue));
            foreach (PropertyInfo property in properties)
                FillProperty(token.ToObject<JObject>(), obj, property);
        }
    }
}
