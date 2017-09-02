using System;

namespace IGDBLib.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IGDBValue : Attribute
    {
        public IGDBValue(string value)
        {
            Value = value;
        }

        /// <summary>
        /// IGDB's Value name
        /// </summary>
        public string Value { get; private set; }
    }
}
