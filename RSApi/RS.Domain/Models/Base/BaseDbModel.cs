using Dapper;

namespace RS.Domain.Models
{
    /// <summary>
    /// The base database model.
    /// </summary>
    /// <seealso cref="BaseModel" />
    [System.Serializable]
    public class BaseDbModel : BaseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether skip timestamp.
        /// </summary>
        /// <value>
        /// A value indicating whether skip timestamp.
        /// </value>
        [IgnoreInsert]
        [IgnoreSelect]
        [IgnoreUpdate]
        public virtual bool SkipTimestamp { get; set; }
    }
}
