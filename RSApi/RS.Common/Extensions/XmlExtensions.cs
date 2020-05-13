using System;
using System.Xml;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The XML extensions.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetValue(this XmlNode value)
        {
            return
                value != null
                    ? value.InnerText
                    : String.Empty;
        }
    }
}