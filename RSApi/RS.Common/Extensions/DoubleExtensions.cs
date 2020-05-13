using System;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The double extensions.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double? ToDouble(this object value)
        {
            if (value == null)
                return null;
            if (value is double)
                return (double) value;

            double result;
            if (String.IsNullOrEmpty(value.ToString()) || !double.TryParse(value.ToString(), out result))
                return null;

            return result;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static double ToDouble(this object value, double defaultValue)
        {
            var result = value.ToDouble();
            return result ?? defaultValue;
        }

        /// <summary>
        /// To the rounded.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digits">The digits.</param>
        /// <returns></returns>
        public static double ToRounded(this double value, int digits)
        {
            return Math.Round(value, digits, MidpointRounding.ToEven);
        }
    }
}