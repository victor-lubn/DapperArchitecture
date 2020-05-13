using System.Collections.Generic;

namespace RS.Common.Helpers
{
    /// <summary>
    /// The dynamic helper.
    /// </summary>
    public static class DynamicHelper
    {
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void SetProperties(dynamic target, dynamic source)
        {
            var targetItems = target as IDictionary<string, object>;
            if (targetItems == null)
                return;

            var sourceItems = source as IDictionary<string, object>;
            if (sourceItems == null)
                return;

            foreach (var sourceItem in sourceItems)
            {
                targetItems[sourceItem.Key] = sourceItem.Value;
            }
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void SetProperty(dynamic d, string propertyName, object propertyValue)
        {
            var items = d as IDictionary<string, object>;
            if (items == null)
                return;

            items[propertyName] = propertyValue;
        }

        /// <summary>
        /// Removes the property.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static bool RemoveProperty(dynamic d, string propertyName)
        {
            var items = d as IDictionary<string, object>;
            if (items == null)
                return false;

            return items.Remove(propertyName);
        }
    }
}
