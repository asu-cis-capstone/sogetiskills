using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Allows searching for consultants.
    /// </summary>
    public interface ISearchManager
    {
        /// <summary>
        /// Get all consultants and filter the results.
        /// </summary>
        /// <param name="beachStatus">Whether or not the consultant is currently on the beach.  Pass null to ignore the beach status.</param>
        /// <param name="lastName">Filter by the consultant's last name.  The comparison is case insensitive and filters where the consultant's last name contains the string.  Pass null to ignore the last name.</param>
        /// <param name="emailAddress">Filter by the consultant's email.  The comparison is case insensitive and but requires an exact match.  Pass null to ignore the email address.</param>
        /// <param name="skills">A list of skills to filter by.  The comparisons are case insensitive and a consultant only needs to match one skill to be included in the result set.</param>
        /// <returns>A filtered list of consultants.</returns>
        Task<IEnumerable<ConsultantWithSkills>> SearchConsultantsAsync(bool? beachStatus, string lastName, string emailAddress, IEnumerable<string> skills);
    }
}
