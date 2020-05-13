using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using RS.Common.Constants;
using RS.Common.Helpers;
using RS.Domain.Models;
using RS.Repositories.QueryBuilders.Factory.Contracts;

namespace RS.Repositories.QueryFilterBuilder.Base
{
    /// <summary>
    /// The base query filter builder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseQueryFilterBuilder<T> where T : BaseFilterModel
    {
        #region Properties

        /// <summary>
        /// The condition or template.
        /// </summary>
        public const string ConditionOrTemplate = "({0} OR {1})";

        /// <summary>
        /// The condition three or template
        /// </summary>
        public const string ConditionThreeOrTemplate = "({0} OR {1} OR {2})";

        /// <summary>
        /// The condition four or template
        /// </summary>
        public const string ConditionFourOrTemplate = "({0} OR {1} OR {2} OR {3})";

        /// <summary>
        /// The condition AND template.
        /// </summary>
        public const string ConditionAndTemplate = "({0} AND {1})";

        /// <summary>
        /// The condition partial or template.
        /// </summary>
        public const string ConditionPartialOrTemplate = " OR ";

        /// <summary>
        /// The condition equals template.
        /// </summary>
        public const string ConditionEqualsTemplate = "{0}.{1} = {2}";

        /// <summary>
        /// The condition not equals template.
        /// </summary>
        public const string ConditionNotEqualsTemplate = "{0}.{1} <> {2}";

        /// <summary>
        /// The condition gt template.
        /// </summary>
        public const string ConditionGtTemplate = "{0}.{1} > {2}";

        /// <summary>
        /// The condition general lt template.
        /// </summary>
        public const string ConditionGeneralGtTemplate = "{0} > {1}";

        /// <summary>
        /// The condition general le template.
        /// </summary>
        public const string ConditionGeneralGeTemplate = "{0} >= {1}";

        /// <summary>
        /// The condition ge template.
        /// </summary>
        public const string ConditionGeTemplate = "{0}.{1} >= {2}";

        /// <summary>
        /// The condition lt template.
        /// </summary>
        public const string ConditionLtTemplate = "{0}.{1} < {2}";

        /// <summary>
        /// The condition general lt template.
        /// </summary>
        public const string ConditionGeneralLtTemplate = "{0} < {1}";

        /// <summary>
        /// The condition general le template.
        /// </summary>
        public const string ConditionGeneralLeTemplate = "{0} <= {1}";

        /// <summary>
        /// The condition le template.
        /// </summary>
        public const string ConditionLeTemplate = "{0}.{1} <= {2}";

        /// <summary>
        /// The condition like template.
        /// </summary>
        public const string ConditionLikeTemplate = "{0}.{1} LIKE {2}";

        /// <summary>
        /// The condition query like template.
        /// </summary>
        public const string ConditionQueryLikeTemplate = "{0} LIKE {1}";

        /// <summary>
        /// The condition in query template.
        /// </summary>
        public const string ConditionInQueryTemplate = "{0}.{1} IN ({2})";

        /// <summary>
        /// The condition not in query template.
        /// </summary>
        public const string ConditionNotInQueryTemplate = "{0}.{1} NOT IN ({2})";

        /// <summary>
        /// The condition in template.
        /// </summary>
        public const string ConditionInTemplate = "{0}.{1} IN {2}";

        /// <summary>
        /// The condition not in template.
        /// </summary>
        public const string ConditionNotInTemplate = "{0}.{1} NOT IN {2}";

        /// <summary>
        /// The condition is null template.
        /// </summary>
        public const string ConditionIsNullTemplate = "{0}.{1} IS NULL";

        /// <summary>
        /// The condition query is zero template.
        /// </summary>
        public const string ConditionQueryIsZeroTemplate = "({0}) = 0";

        /// <summary>
        /// The condition is null or equals template.
        /// </summary>
        public const string ConditionIsNullOrEqualsTemplate = "({0}.{1} IS NULL OR {0}.{1} = {2})";

        /// <summary>
        /// The condition is null or empty template.
        /// </summary>
        public const string ConditionIsNullOrEmptyTemplate = "({0}.{1} IS NULL OR {0}.{1} = '')";

        /// <summary>
        /// The condition is not null template.
        /// </summary>
        public const string ConditionIsNotNullTemplate = "{0}.{1} IS NOT NULL";

        /// <summary>
        /// The condition is not null and not empty template.
        /// </summary>
        public const string ConditionIsNotNullAndNotEmptyTemplate = "({0}.{1} IS NOT NULL AND {0}.{1} <> '')";

        /// <summary>
        /// The condition sum with separator template.
        /// </summary>
        public const string ConditionSumWithSeparatorTemplate = "{0}.{1} + '{3}' + {0}.{2}";

        /// <summary>
        /// The condition select template.
        /// </summary>
        public const string ConditionSelectTemplate = "SELECT {0} ";

        /// <summary>
        /// The condition template
        /// </summary>
        public const string ConditionFieldValuesWithSeparatorTemplate = "('{0}' + REPLACE(RTRIM({1}.{2}), ' ', '') + '{0}') LIKE '%{0}' + {3} + '{0}%'";

        /// <summary>
        /// Gets the condition union select template.
        /// </summary>
        /// <value>
        /// The condition union select template.
        /// </value>
        public string ConditionUnionSelectTemplate
        {
            get { return String.Concat(Environment.NewLine, " UNION SELECT {0} "); }
        }

        /// <summary>
        /// The condition select in table template.
        /// </summary>
        public const string ConditionSelectInTableTemplate = "SELECT ID FROM (VALUES ({0}) ";

        /// <summary>
        /// The condition start select in table template.
        /// </summary>
        public const string ConditionStartSelectInTableTemplate = ",({0}) ";

        /// <summary>
        /// The condition end select in table template.
        /// </summary>
        public const string ConditionEndSelectInTableTemplate = ") as {0} (ID)";

        /// <summary>
        /// The condition start bracket template.
        /// </summary>
        public string ConditionStartBracketTemplate
        {
            get { return String.Concat("( ", Environment.NewLine); }
        }

        /// <summary>
        /// The condition end bracket template.
        /// </summary>
        public string ConditionEndBracketTemplate 
        {
            get { return String.Concat(Environment.NewLine, " )"); }
        }
        
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        protected T Filter { get; set; }

        /// <summary>
        /// Gets or sets the query builder factory.
        /// </summary>
        /// <value>
        /// The query builder factory.
        /// </value>
        protected IQueryBuilderFactory QueryBuilderFactory { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public IList<string> Conditions { get; protected set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object Param { get; protected set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        protected string Alias { get; set; }

        /// <summary>
        /// Gets or sets the second alias.
        /// </summary>
        /// <value>
        /// The second alias.
        /// </value>
        protected string SecondAlias { get; set; }

        /// <summary>
        /// Gets or sets the trird alias.
        /// </summary>
        /// <value>
        /// The trird alias.
        /// </value>
        protected string ThirdAlias { get; set; }

        /// <summary>
        /// Gets or sets the fourth alias.
        /// </summary>
        /// <value>
        /// The fourth alias.
        /// </value>
        protected string FourthAlias { get; set; }

        /// <summary>
        /// Gets or sets the fifth alias.
        /// </summary>
        /// <value>
        /// The fifth alias.
        /// </value>
        protected string FifthAlias { get; set; }

        /// <summary>
        /// Gets or sets the sixth alias.
        /// </summary>
        /// <value>
        /// The sixth alias.
        /// </value>
        protected string SixthAlias { get; set; }

        /// <summary>
        /// Gets or sets the seventh alias.
        /// </summary>
        /// <value>
        /// The seventh alias.
        /// </value>
        protected string SeventhAlias { get; set; }

        /// <summary>
        /// The search full name pattern
        /// </summary>
        private readonly char[] SearchFullNamePattern = { ' ', ',' };

        #endregion

        #region BaseQueryFilterBuilder
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryFilterBuilder{T}" /> class.
        /// </summary>
        /// <param name="queryBuilderFactory">The query builder factory.</param>
        /// <param name="dataServiceFactory">The data service factory.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="secondAlias">The additional alias.</param>
        /// <param name="thirdAlias">The third alias.</param>
        /// <param name="fourthAlias">The fourth alias.</param>
        /// <param name="fifthAlias">The fifth alias.</param>
        /// <param name="sixthAlias">The sixth alias.</param>
        /// <param name="seventhAlias">The seventh alias.</param>
        public BaseQueryFilterBuilder(
            IQueryBuilderFactory queryBuilderFactory,
            T filter,
            string alias,
            object param = null,
            string secondAlias = "",
            string thirdAlias = "",
            string fourthAlias = "",
            string fifthAlias = "",
            string sixthAlias = "",
            string seventhAlias = "")
        {
            Check.Argument.IsNotNull(queryBuilderFactory, "queryBuilderFactory");
            QueryBuilderFactory = queryBuilderFactory;

            Filter = filter;
            Conditions = new List<string>();
            Param = param ?? new ExpandoObject();
            Alias = alias;
            SecondAlias = secondAlias;
            ThirdAlias = thirdAlias;
            FourthAlias = fourthAlias;
            FifthAlias = fifthAlias;
            SixthAlias = sixthAlias;
            SeventhAlias = seventhAlias;
        }
        #endregion

        /// <summary>
        /// Gets the field values with separator condition.
        /// </summary>
        /// <param name="entityIds">The entity ids.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="field">The field.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public string GetFieldValuesWithSeparatorCondition(IList<string> entityIds, string parameters, object param, string alias, string field, char separator = ',')
        {
            var ids = entityIds.ToList();
            var commonCondition = new StringBuilder();
            var queryCondition = new StringBuilder();

            DynamicHelper.SetProperty(param, Parameters.StringValues, ids);

            commonCondition.Append(String.Format(ConditionIsNotNullAndNotEmptyTemplate, alias, field));
            var index = 1;
            foreach (var id in ids)
            {
                queryCondition.Append(String.Format(ConditionFieldValuesWithSeparatorTemplate, separator, alias, field, "@" + String.Concat(parameters, index)));
                if (index < ids.Count)
                    queryCondition.Append(" OR ");

                index++;
            }

            return $"{ commonCondition } AND ({ queryCondition })";
        }

        /// <summary>
        /// Gets the list ids query condition.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="entityIds">The entity ids.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public string GetListIdsQueryCondition<K>(IList<K> entityIds, string parameters, object param, string alias, string field) where K : struct
        {
            var ids = entityIds.ToList();

            var condition = new StringBuilder();
            condition.Append(ConditionStartBracketTemplate);

            if (ids.Count < DbConstants.MaxParametersPerCommand)
            {
                while (ids.Any())
                {
                    DynamicHelper.SetProperty(
                        param,
                        String.Concat(parameters, ids.Count),
                        ids.Take(DbConstants.MaxParametersPerCommand).ToList());

                    condition.Append(String.Format(ConditionInTemplate, alias, field, "@" + String.Concat(parameters, ids.Count)));
                    ids.RemoveRange(
                        0,
                        ids.Count > DbConstants.MaxParametersPerCommand ? DbConstants.MaxParametersPerCommand : ids.Count);
                    if (ids.Any())
                        condition.Append(ConditionPartialOrTemplate);
                }
                condition.Append(ConditionEndBracketTemplate);
                return condition.ToString();
            }
            
            for (var i = 0; i < ids.Count; i++)
            {
                condition.Append(i == 0
                    ? String.Format(ConditionSelectInTableTemplate, ids[i])
                    : String.Format(ConditionStartSelectInTableTemplate, ids[i]));
            }
            condition.Append(String.Format(ConditionEndSelectInTableTemplate, parameters));
            condition.Append(ConditionEndBracketTemplate);

            return String.Format(ConditionInQueryTemplate, alias, field, condition);
        }

        /// <summary>
        /// Fulls the name search.
        /// </summary>
        /// <param name="firstNameField">The first name field.</param>
        /// <param name="lastNameField">The last name field.</param>
        /// <param name="uniqueIdField">The unique identifier field.</param>
        /// <param name="searchString">The search string.</param>
        protected void FullNameSearch(string firstNameField, string lastNameField, string uniqueIdField, string searchString)
        {
            var parts = new List<string>();

            if (!String.IsNullOrWhiteSpace(searchString))
                parts = searchString.Trim().Split(SearchFullNamePattern, StringSplitOptions.RemoveEmptyEntries).Take(3).ToList();

            switch (parts.Count)
            {
                case 0:
                    DynamicHelper.SetProperty(Param, Parameters.Query, "-1");
                    Conditions.Add(SearchNameLike(uniqueIdField, Parameters.Query));
                    break;
                case 1:
                    DynamicHelper.SetProperty(Param, Parameters.Query, parts[0]);
                    Conditions.Add(String.Format(
                        ConditionThreeOrTemplate,
                        SearchNameLike(firstNameField, Parameters.Query),
                        SearchNameLike(lastNameField, Parameters.Query),
                        SearchNameLike(uniqueIdField, Parameters.Query)));
                    break;
                case 2:
                    DynamicHelper.SetProperty(Param, Parameters.Query, String.Join(" ", parts));
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery1, parts[0]);
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery2, parts[1]);
                    Conditions.Add(String.Format(
                        ConditionFourOrTemplate,
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery1), SearchNameLike(lastNameField, Parameters.SearchQuery2)),
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery2), SearchNameLike(lastNameField, Parameters.SearchQuery1)),
                        SearchNameLike(firstNameField, Parameters.Query),
                        SearchNameLike(lastNameField, Parameters.Query)));
                    break;
                case 3:
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery1, $"{parts[0]} {parts[1]}");
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery2, parts[2]);
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery3, parts[0]);
                    DynamicHelper.SetProperty(Param, Parameters.SearchQuery4, $"{parts[1]} {parts[2]}");
                    Conditions.Add(String.Format(
                        ConditionFourOrTemplate,
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery1), SearchNameLike(lastNameField, Parameters.SearchQuery2)),
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery2), SearchNameLike(lastNameField, Parameters.SearchQuery1)),
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery3), SearchNameLike(lastNameField, Parameters.SearchQuery4)),
                        String.Format(ConditionAndTemplate, SearchNameLike(firstNameField, Parameters.SearchQuery4), SearchNameLike(lastNameField, Parameters.SearchQuery3))));
                    break;
            }
        }

        /// <summary>
        /// Students the search like.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="parameter">The query constant.</param>
        /// <returns></returns>
        private string SearchNameLike(string field, string parameter)
        {
            return String.Format(ConditionLikeTemplate, Alias, field, "@" + parameter + " + '%'");
        }
    }
}