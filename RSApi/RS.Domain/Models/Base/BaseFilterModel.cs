namespace RS.Domain.Models
{
    /// <summary>
    /// The base filter model.
    /// </summary>
    public class BaseFilterModel : BaseWebModel
    {
        /// <summary>
        /// Gets or sets the quick filter identifier.
        /// </summary>
        /// <value>
        /// The quick filter identifier.
        /// </value>
        public string QuickFilterId { get; set; }
    }
}