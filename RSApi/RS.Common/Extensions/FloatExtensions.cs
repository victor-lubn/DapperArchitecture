using System;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The float extensions.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float? ToFloat(this object value)
        {
            if (value == null)
                return null;
            if (value is float)
                return (float)value;

            float result;
            if (String.IsNullOrEmpty(value.ToString()) || !float.TryParse(value.ToString(), out result))
                return null;

            return result;
        }

        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static float ToFloat(this object value, float defaultValue)
        {
            var result = value.ToFloat();
            return !result.HasValue ? defaultValue : result.Value;
        }
    }
}