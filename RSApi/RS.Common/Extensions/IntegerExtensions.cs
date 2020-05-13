using System;
using System.Globalization;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The integer extensions.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this int value)
        {
            return value > 0;
        }

        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this int? value)
        {
            return value.HasValue && value.Value.IsPositive();
        }

        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int? ToInt(this object value)
        {
            if (value == null)
                return null;
            if (value is int || value is Enum)
                return (int)value;

            int result;
            if (int.TryParse(value.ToString(), out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ToInt(this object value, int defaultValue)
        {
            var result = value.ToInt();
            return !result.HasValue ? defaultValue : result.Value;
        }

        /// <summary>
        /// To the ordinal string.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToOrdinalString(this int number)
        {
            var ones = number % 10;
            var tens = Math.Floor(number / 10f) % 10;
            if (tens == 1)
                return number + "th";

            switch (ones)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static string GetMonthName(this int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}