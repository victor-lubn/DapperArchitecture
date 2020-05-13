using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RS.Common.Helpers
{
    /// <summary>
    /// The reflection helper.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets the method definition.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodDefinition<TSource>(Expression<Action<TSource>> method)
        {
            var methodInfo = GetMethod(method);
            return !methodInfo.IsGenericMethod ? methodInfo : methodInfo.GetGenericMethodDefinition();
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">method</exception>
        public static MethodInfo GetMethod<TSource>(Expression<Action<TSource>> method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            return ((MethodCallExpression)method.Body).Method;
        }

        /// <summary>
        /// Gets the method definition.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static MethodInfo GetMethodDefinition(Expression<Action> method)
        {
            var methodInfo = GetMethod(method);
            return !methodInfo.IsGenericMethod ? methodInfo : methodInfo.GetGenericMethodDefinition();
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">method</exception>
        public static MethodInfo GetMethod(Expression<Action> method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            return ((MethodCallExpression)method.Body).Method;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">property</exception>
        public static MemberInfo GetProperty<TSource, TResult>(Expression<Func<TSource, TResult>> property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            return ((MemberExpression)property.Body).Member;
        }

        /// <summary>
        /// Gets the type of the property or field.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns></returns>
        public static Type GetPropertyOrFieldType(this MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.PropertyType;
            var fieldInfo = memberInfo as FieldInfo;
            return fieldInfo != null ? fieldInfo.FieldType : null;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TPropertyType">The type of the property type.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string GetString<TSource, TPropertyType>(Expression<Func<TSource, TPropertyType>> property)
        {
            return GetProperty(property).Name;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="inherit">If set to <c>true</c> then inherit.</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TSource, TAttribute>(string propertyName = null, bool inherit = false)
            where TAttribute : Attribute
        {
            var type = typeof(TSource);
            if (propertyName != null)
            {
                var propertyInfo = type.GetProperty(propertyName);
                return GetAttribute<TAttribute>(propertyInfo, inherit);
            }
            var attributes = type.GetCustomAttributes(typeof(TAttribute), inherit);
            if (attributes.Length > 0)
            {
                return (TAttribute)attributes[0];
            }
            return null;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="inherit">If set to <c>true</c> then inherit.</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(PropertyInfo propertyInfo, bool inherit = false)
            where TAttribute : Attribute
        {
            if (propertyInfo != null)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(TAttribute), inherit);
                if (attributes.Length > 0)
                {
                    return (TAttribute)attributes[0];
                }
                return null;
            }
            return null;
        }
    }
}
