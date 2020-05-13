using System;
using System.Linq;
using System.Linq.Expressions;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The expression extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Invert expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var inverted = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(inverted, expression.Parameters);
        }

        /// <summary>
        /// Merge two expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionLeft">The expression left.</param>
        /// <param name="expressionRight">The expression right.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expressionLeft,
            Expression<Func<T, bool>> expressionRight)
        {
            return expressionLeft.Compose(expressionRight, Expression.AndAlso);
        }

        /// <summary>
        /// Merge two expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionLeft">The expression left.</param>
        /// <param name="expressionRight">The expression right.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expressionLeft,
            Expression<Func<T, bool>> expressionRight)
        {
            return expressionLeft.Compose(expressionRight, Expression.OrElse);
        }

        /// <summary>
        /// Composes the specified first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressionLeft">The first.</param>
        /// <param name="expressionRight">The second.</param>
        /// <param name="merge">The merge.</param>
        /// <returns></returns>
        private static Expression<T> Compose<T>(
            this Expression<T> expressionLeft,
            Expression<T> expressionRight,
            Func<Expression, Expression, Expression> merge)
        {
            var mapping = expressionLeft.Parameters
                .Select((paramLeft, index) => new { paramLeft, paramRight = expressionRight.Parameters[index] })
                .ToDictionary(p => p.paramRight, p => p.paramLeft);

            var expressionRightBody = new ExpressionRewriter(mapping).Visit(expressionRight.Body);
            return Expression.Lambda<T>(merge(expressionLeft.Body, expressionRightBody), expressionLeft.Parameters);
        }
    }
}