using System;
using Dapper;

namespace RS.Domain.Models.Data
{
    /// <summary>
    /// AppUser
    /// </summary>
    /// <seealso cref="RS.Domain.Models.BaseDbModel" />
    [Table("AppOrder")]
    public class AppOrder : BaseDbModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid AppOrderId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid AppUserId { get; set; }
        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        /// <value>
        /// The name of the order.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The name of the create date.
        /// </value>
        public DateTime? CreateDate { get; set; }
    }
}
