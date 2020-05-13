using System;
using System.Text.RegularExpressions;
using RS.Common.Constants;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The unique identifier extensions.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// The base date ticks.
        /// </summary>
        private static readonly long BaseDateTicks = new DateTime(1900, 1, 1).Ticks;

        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this Guid value)
        {
            return value != Guid.Empty;
        }

        /// <summary>
        /// Determines whether Is positive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsPositive(this Guid? value)
        {
            return value.HasValue && value.Value.IsPositive();
        }

        /// <summary>
        /// To the unique identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid? ToGuid(this object value)
        {
            Guid? result = null;
            if (value != null)
            {
                var regex = new Regex(RegularExpressionConstants.Guid);
                var match = regex.Match(value.ToString());
                if (match.Success)
                {
                    result = new Guid(value.ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// To the unique identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid? ToGuid(this string value)
        {
            Guid? result = null;
            if (!String.IsNullOrEmpty(value))
            {
                var regex = new Regex(RegularExpressionConstants.Guid);
                var match = regex.Match(value);
                if (match.Success)
                {
                    result = new Guid(value);
                }
            }

            return result;
        }

        /// <summary>
        /// To 32 digits string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string To32DigitsString(this Guid value)
        {
            return value.ToString("N");
        }

        /// <summary>
        /// Generate a new <see cref="Guid" /> using the comb algorithm.
        /// https://github.com/nhibernate/nhibernate-core/blob/master/src/NHibernate/Id/GuidCombGenerator.cs
        /// </summary>
        /// <returns></returns>
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime now = DateTime.UtcNow;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - BaseDateTicks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            
            return new Guid(guidArray);
        }
    }
}