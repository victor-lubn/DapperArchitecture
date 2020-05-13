using System;
using System.Collections.Generic;
using System.Text;

namespace RS.Domain.Models.Filters
{
    public class AppUserFilter : BaseFilterModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the app users ids.
        /// </summary>
        /// <value>
        /// The app users ids.
        /// </value>
        public List<int> AppUsersIds { get; set; }
    }
}
