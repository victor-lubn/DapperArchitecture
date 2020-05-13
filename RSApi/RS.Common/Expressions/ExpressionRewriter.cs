using System.Collections.Generic;
using System.Linq.Expressions;

namespace RS.Common.Extensions
{
    /// <summary>
    /// The expression rewriter.
    /// </summary>
    public class ExpressionRewriter : ExpressionVisitor
    {
        /// <summary>
        /// The parameter mapping.
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> ParameterMapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionRewriter"/> class.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public ExpressionRewriter(Dictionary<ParameterExpression, ParameterExpression> mapping)
        {
            ParameterMapping = mapping ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Visits the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            parameter = ParameterMapping[parameter] ?? parameter;
            return base.VisitParameter(parameter);
        }
    }
}
