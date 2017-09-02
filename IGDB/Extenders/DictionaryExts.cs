using System.Collections.Generic;

namespace IGDBLib.Extenders
{
    public static class DictionaryExts
    {
        /// <summary>
        /// Merge two dictionaries
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="baseDic">Base dictionary</param>
        /// <param name="newDic">Dictionary to add</param>
        /// <param name="overrideExisting">override existing keys</param>
        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> baseDic, Dictionary<TKey, TValue> newDic, bool overrideExisting = false)
        {
            if (newDic != null && newDic.Count > 0)
            {
                foreach (KeyValuePair<TKey, TValue> value in baseDic)
                {
                    if (baseDic.ContainsKey(value.Key))
                    {
                        if (overrideExisting)
                        {
                            baseDic.Remove(value.Key);
                            baseDic.Add(value.Key, value.Value);
                        }
                        continue;
                    }
                    baseDic.Add(value.Key, value.Value);
                }
            }
        }
    }
}
