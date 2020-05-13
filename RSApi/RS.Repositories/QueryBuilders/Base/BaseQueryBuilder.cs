using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RS.Repositories.QueryBuilders.Base.Contracts;
using RS.Repositories.QueryBuilders.Factory.Contracts;

namespace RS.Repositories.QueryBuilders.Base
{
    /// <summary>
    /// The base query builder.
    /// </summary>
    public class BaseQueryBuilder : IBaseQueryBuilder
    {
        #region Properties

        /// <summary>
        /// Gets or sets the query builder factory.
        /// </summary>
        /// <value>
        /// The query builder factory.
        /// </value>
        protected IQueryBuilderFactory QueryBuilderFactory { get; set; }

        #endregion

        #region BaseQueryBuilder
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryBuilder"/> class.
        /// </summary>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        public BaseQueryBuilder(IQueryBuilderFactory queryBuilderFactory)
        {
            QueryBuilderFactory = queryBuilderFactory;
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Appends the conditions.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="conditions">The conditions.</param>
        protected void AppendConditions(StringBuilder builder, IList<string> conditions)
        {
            foreach (var condition in conditions)
                AppendCondition(builder, condition);

        }

        /// <summary>
        /// Appends the condition.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="condition">The condition.</param>
        protected void AppendCondition(StringBuilder builder, string condition)
        {
            builder.Append(Environment.NewLine);
            builder.Append($"AND {condition}");
        }

        /// <summary>
        /// The group by SQL.
        /// </summary>
        private const string GroupBySql = @"GROUP BY {0}";
        /// <summary>
        /// Appends the grouping.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="groups">The groups.</param>
        protected void AppendGrouping(StringBuilder builder, string groups)
        {
            if (String.IsNullOrEmpty(groups))
                return;

            builder.Append(Environment.NewLine);
            builder.Append(String.Format(GroupBySql, groups));
        }

        /// <summary>
        /// The having SQL.
        /// </summary>
        private const string HavingSql = @"HAVING {0}";

        /// <summary>
        /// Appends the having.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="condition">The condition.</param>
        protected void AppendHaving(StringBuilder builder, string condition)
        {
            if (String.IsNullOrEmpty(condition))
                return;

            builder.Append(Environment.NewLine);
            builder.Append(String.Format(HavingSql, condition));
        }

        /// <summary>
        /// The order by SQL.
        /// </summary>
        public const string OrderBySql = @"ORDER BY {0}";

        /// <summary>
        /// The skip take SQL.
        /// </summary>
        private const string SkipTakeSql = @"OFFSET (@Skip) ROWS FETCH NEXT (@Take) ROWS ONLY";
        /// <summary>
        /// Appends the skip take SQL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected void AppendSkipTakeSql(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
            builder.Append(SkipTakeSql);
        }

        /// <summary>
        /// The arithabort SQL.
        /// </summary>
        private const string ArithabortSql = @"SET ARITHABORT ON ";

        /// <summary>
        /// Prepends the arithabort SQL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected void PrependArithabortSql(StringBuilder builder)
        {
            builder.Insert(0, Environment.NewLine);
            builder.Insert(0, ArithabortSql);
        }

        /// <summary>
        /// The recompile SQL.
        /// </summary>
        private const string WithNoLockSql = @" WITH (NOLOCK) ";

        /// <summary>
        /// Appends the with no lock SQL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="argsPosition">The arguments position.</param>
        protected void AppendWithNoLockSql(ref StringBuilder builder, int[] argsPosition = null)
        {
            var defaultArgs = 20;
            var args = new object[defaultArgs];
            if (argsPosition == null)
            {
                for (var i = 0; i < defaultArgs; i++)
                    args[i] = WithNoLockSql;
            }
            else
            {
                for (var i = 0; i < defaultArgs; i++)
                {
                    if (argsPosition.Contains(i))
                        args[i] = WithNoLockSql;
                    else
                        args[i] = String.Empty;
                }
            }
            
            builder = new StringBuilder(String.Format(builder.ToString(), args));
        }

        /// <summary>
        /// The recompile SQL.
        /// </summary>
        private const string RecompileSql = @" OPTION (RECOMPILE);";

        /// <summary>
        /// Appends the recompile SQL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected void AppendRecompileSql(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
            builder.Append(RecompileSql);
        }

        #endregion
    }
}