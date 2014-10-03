using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for tags (skills).
    /// </summary>
    public interface ITagManager
    {
        /// <summary>
        /// Load all of the tags that have been applied to a consultant.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to load tags for.</param>
        /// <returns>All the tags that have been applied to consultant</returns>
        Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(int consultantId);

        /// <summary>
        /// Load all of the canonical tags from the database.  Account executives maintain the list
        /// of canonical tags.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tag>> LoadCanonicalTagsAsync();
    }
}
