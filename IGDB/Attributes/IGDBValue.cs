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

        public IGDBValue(IGDBFields field)
        {
            Value = field.ToString().ToLower();
        }

        /// <summary>
        /// IGDB's Value name
        /// </summary>
        public string Value { get; private set; }

        public IGDBFields Field => (IGDBFields)Enum.Parse(typeof(IGDBFields), Value.ToUpper());
    }
}
