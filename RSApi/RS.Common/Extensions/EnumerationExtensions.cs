using System;
using System.Resources;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The enumeration extensions.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// To the enum nullable.
        /// </summary>
        /// <typeparam name="TEnumType">The type of the enum type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static TEnumType? ToEnumNullable<TEnumType>(this string value)
            where TEnumType : struct
        {
            if (String.IsNullOrEmpty(value))
                return null;

            int result;
            if (Int32.TryParse(value, out result) && Enum.IsDefined(typeof(TEnumType), result))
                return (TEnumType) Enum.Parse(typeof(TEnumType), value);

            if (!Enum.IsDefined(typeof(TEnumType), value))
                return null;

            return (TEnumType) Enum.Parse(typeof(TEnumType), value);
        }

        /// <summary>
        /// To the enum.
        /// </summary>
        /// <typeparam name="TEnumType">The type of the enum type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static TEnumType ToEnum<TEnumType>(this string value)
            where TEnumType : struct
        {
            return (TEnumType) Enum.Parse(typeof(TEnumType), value);
        }

        /// <summary>
        /// To the resource string.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <returns></returns>
        public static string ToResourceString<TEnum>(this TEnum value, ResourceManager resourceManager) where TEnum : struct
        {
            return resourceManager.GetString(String.Format("{0}_{1}", typeof(TEnum).Name, value));
        }

        /// <summary>
        /// To the resource string.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string ToResourceStringNullable<TEnum>(this TEnum? value, ResourceManager resourceManager, string defaultValue = null) where TEnum : struct
        {
            return value.HasValue
                ? value.Value.ToResourceString(resourceManager)
                : defaultValue ?? String.Empty;
        }

        /// <summary>
        /// Toes the resource string.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="resourceKeyPrefix">The resource key prefix.</param>
        /// <returns></returns>
        public static string ToResourceString(this Enum e, ResourceManager resourceManager, string resourceKeyPrefix)
        {
            return e == null
                ? String.Empty
                : resourceManager.GetString(String.Format("{0}{1}", resourceKeyPrefix, e));
        }
    }
}