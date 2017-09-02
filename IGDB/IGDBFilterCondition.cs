using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGDBLib
{
    /// <summary>
    /// IGDB Filters post fixes <see cref="https://igdb.github.io/api/references/filters/"/>
    /// </summary>
    public enum IGDBFilterCondition
    {
        /// <summary>
        /// Equal: Exact match equal
        /// </summary>
        EQ,
        /// <summary>
        /// Not Equal: Exact match equal
        /// </summary>
        NOT_EQ,
        /// <summary>
        /// Greater Than: Works only on numbers
        /// </summary>
        GT,
        /// <summary>
        /// Greater Than Or Equal: Works only on numbers
        /// </summary>
        GTE,
        /// <summary>
        /// Less Than: Works only on numbers
        /// </summary>
        LT,
        /// <summary>
        /// Less Than Or Equal: Works only on numbers
        /// </summary>
        LTE,
        /// <summary>
        /// Prefix Of A Value: Works only on strings
        /// </summary>
        PREFIX,
        /// <summary>
        /// The Value Is Not Null
        /// </summary>
        EXISTS,
        /// <summary>
        /// The Value Is Null
        /// </summary>
        NOT_EXISTS,
        /// <summary>
        /// The value exists winthin the (comma separated) array (AND between values)
        /// </summary>
        IN,
        /// <summary>
        /// The value must not exists within the (comma separated) array (AND between values)
        /// </summary>
        NOT_IN,
        /// <summary>
        /// The value has any within the (comma separated) array (OR between values).
        /// </summary>
        ANY
    }
}
