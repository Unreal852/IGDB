using IGDBLib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGDBLib
{
    public class IGDBParams
    {
        private List<IGDBFilter> m_filters = new List<IGDBFilter>();

        public IGDBParams()
        {

        }

        public IGDBFilter[] Filters => m_filters.ToArray();

        /// <summary>
        /// Fields Params
        /// </summary>
        public IGDBFields[] Fields { get; private set; }

        public int Limit { get; set; } = 1;

        /// <summary>
        /// Set Fields
        /// </summary>
        /// <param name="fields">Fields</param>
        public void SetFields(params IGDBFields[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Set Fields by object properties ( properties must have the IGDBValue attribute set )
        /// </summary>
        /// <param name="obj">Object</param>
        public void SetFields(Type obj)
        {
            Fields = TypeHelper.GetPropertiesFields(obj);
        }

        /// <summary>
        /// Set Filters
        /// </summary>
        /// <param name="filters">Filters</param>
        public void SetFilters(params IGDBFilter[] filters)
        {
            m_filters.AddRange(filters);
        }

        /// <summary>
        /// Add Filter
        /// </summary>
        /// <param name="filter">Filter</param>
        public void AddFilter(IGDBFilter filter)
        {
            m_filters.Add(filter);
        }

        /// <summary>
        /// Build params and return URL params
        /// </summary>
        /// <returns>URL params</returns>
        public string Build()
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            if (Fields?.Length > 0)
            {
                if (Fields.Length >= Enum.GetNames(typeof(IGDBFields)).Length)
                    sb.Append("fields=*");
                else
                {
                    sb.Append("fields=");
                    foreach (IGDBFields field in Fields)
                    {
                        if (index > 0)
                            sb.Append($",{field.ToString().ToLower()}");
                        else
                            sb.Append($"{field.ToString().ToLower()}");
                        index++;
                    }
                }
                index = 0;
            }
            if(m_filters?.Count > 0)
            {
                foreach(IGDBFilter filter in Filters) //TODO Check if filter values aren't null
                    sb.Append($"&filter[{filter.Field}][{filter.FilterCondition.ToString().ToLower()}]={filter.Value}");
            }
            return sb.ToString();
        }
    }
}
