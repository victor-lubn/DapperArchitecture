using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using RS.Common.Constants;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Equalses the ignore case.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string target, string value)
        {
            return target.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether Contains ignore case.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string target, string value)
        {
            return target.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        /// <summary>
        /// Startses the with ignore case.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string target, string value)
        {
            return target.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Endses the with ignore case.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string target, string value)
        {
            return target.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Nulls the safe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string NullSafe(this string value)
        {
            return (value ?? String.Empty).Trim();
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string FormatWith(this string value, params object[] args)
        {
            return String.Format(value, args);
        }

        /// <summary>
        /// Lefts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            value = value.NullSafe();
            return
                value.Length > length
                    ? value.Substring(0, length)
                    : value;
        }

        /// <summary>
        /// Rights the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            value = value.NullSafe();
            return
                value.Length > length
                    ? value.Substring(value.Length - length, length)
                    : value;
        }

        /// <summary>
        /// Trancates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string Trancate(this string value, int maxLength)
        {
            var adjustedValue = value.NullSafe();

            if (adjustedValue.Length <= maxLength)
                return adjustedValue;

            adjustedValue = adjustedValue.Substring(0, maxLength);

            var lastSpaceIndex = adjustedValue.LastIndexOf(" ", StringComparison.Ordinal);
            if (lastSpaceIndex != -1)
                adjustedValue = adjustedValue.Substring(0, lastSpaceIndex);

            return String.Concat(adjustedValue, " ...");
        }

        /// <summary>
        /// Equalses to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public static bool EqualsTo(
            this string value,
            string valueToCompare,
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            return
                (String.IsNullOrEmpty(value) && String.IsNullOrEmpty(valueToCompare)) ||
                String.Equals(value, valueToCompare, stringComparison);
        }

        /// <summary>
        /// To the HTML.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToHtml(this string value)
        {
            return Regex.Replace(value.NullSafe(), @"\r\n?|\n", "<br/>");
        }

        /// <summary>
        /// To the safe XML.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToSafeXml(this string value)
        {
            return value
                .Replace("&", "&amp;")
                .Replace(">", "&gt;")
                .Replace("<", "&lt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        /// <summary>
        /// To the safe js text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToSafeJsText(this string value)
        {
            return value
                .NullSafe()
                .Replace(Environment.NewLine, String.Empty)
                .Replace("\n", "<br />")
                .Replace("\"", "&#34;")
                .Replace("'", "’");
        }

        /// <summary>
        /// Toes the fix length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <param name="leadingChar">The leading char.</param>
        /// <returns></returns>
        public static string ToFixLength(this string value, int length, char leadingChar)
        {
            while (value.Length < length)
                value = String.Concat(leadingChar, value);

            return value;
        }

        /// <summary>
        /// Toes the safe file name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToSafeFileName(this string value)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var pattern = String.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(value, pattern, String.Empty);
        }

        /// <summary>
        /// Toes the safe SQL name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToSafeSqlName(this string value)
        {
            if (!value.StartsWith("["))
                value = String.Concat("[", value);

            if (!value.EndsWith("]"))
                value = String.Concat(value, "]");

            return value;
        }

        /// <summary>
        /// Froms the comma separated string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IList<string> FromCommaSeparatedString(this string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return new List<string>();

            return Regex.Split(value, CommonConstants.CommaSeparator)
                .Select(d => d.Trim())
                .Where(d => !String.IsNullOrWhiteSpace(d))
                .ToList();
        }

        /// <summary>
        /// Strips the tags.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <returns></returns>
        public static string StripTags(this string value, string replaceText = null)
        {
            return
                !String.IsNullOrEmpty(value)
                    ? Regex.Replace(value, "<[^>]*>|&nbsp;", replaceText ?? String.Empty)
                    : value;
        }

        /// <summary>
        /// Strips the tags.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<int> CommaSeparatedStringToArray(this string value)
        {
            return String.IsNullOrWhiteSpace(value) ? new List<int>() : value.Split(',').Select(int.Parse).ToList();
        }

        /// <summary>
        /// Determines whether [is valid email].
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if [is valid email] [the specified email]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}