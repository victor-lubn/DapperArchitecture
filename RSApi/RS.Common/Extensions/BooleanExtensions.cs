using System;
using RS.Common.Enums;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The boolean extensions.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool? ToBool(this object value)
        {
            if (value == null)
                return null;
            if (value is bool)
                return (bool) value;

            bool result;
            if (!Boolean.TryParse(value.ToString(), out result))
                return null;

            return result;
        }

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static bool ToBool(this object value, bool defaultValue)
        {
            var result = value.ToBool();
            return result ?? defaultValue;
        }

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool? ToBool(this BooleanEnum value)
        {
            return value == BooleanEnum.Yes;
        }

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool? ToBool(this BooleanEnum? value)
        {
            return value.HasValue ? value.Value.ToBool() : null;
        }

        /// <summary>
        /// To the boolean result.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static BooleanEnum ToBooleanResult(this bool value)
        {
            return value ? BooleanEnum.Yes : BooleanEnum.No;
        }

        /// <summary>
        /// To the JS value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToJsValue(this bool value)
        {
            return value ? "true" : "false";
        }
    }
}