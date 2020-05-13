using System;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The decimal extensions.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this decimal value)
        {
            return value > 0;
        }

        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this decimal? value)
        {
            return value.HasValue && value.Value.IsPositive();
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal? ToDecimal(this object value)
        {
            if (value == null)
                return null;
            if (value is decimal)
                return (decimal) value;

            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;

            return null;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object value, decimal defaultValue)
        {
            var result = value.ToDecimal();
            return result ?? defaultValue;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimals">The decimals.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public static decimal? ToDecimal(this object value, int decimals, MidpointRounding mode)
        {
            var result = value.ToDecimal();
            return result.HasValue ? Math.Round(result.Value, decimals, mode) : (decimal?) null;
        }

        /// <summary>
        /// To the rounded.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns></returns>
        public static decimal ToRounded(this decimal value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.ToEven);
        }

        /// <summary>
        /// To the rounded.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns></returns>
        public static decimal? ToRounded(this object o, int decimals)
        {
            var value = o.ToDecimal();
            return value.HasValue ? Math.Round(value.Value, decimals, MidpointRounding.ToEven) : (decimal?)null;
        }

        /// <summary>
        /// To the percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToPercent(this decimal value)
        {
            return (int)value.ToPercentDecimal();
        }

        /// <summary>
        /// To the percent decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal ToPercentDecimal(this decimal value)
        {
            var result = Math.Round(100 * value, 2);
            if (result > 100)
                return 100;
            return result < 0 ? 0 : result;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this decimal? value, string format)
        {
            return value.HasValue ? value.Value.ToString(format) : String.Empty;
        }
    }
}