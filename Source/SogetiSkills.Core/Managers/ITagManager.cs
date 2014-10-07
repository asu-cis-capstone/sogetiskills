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
        /// <returns>All canonical tags ordered by keyword.</returns>
        Task<IEnumerable<Tag>> LoadCanonicalTagsAsync();

        /// <summary>
        /// Inserts a new canonical tag.
        /// </summary>
        /// <param name="keyword">The tag's keyword.</param>
        /// <param name="skillDescription">An optional tag description.</param>
        Task AddCanonicalTagAsync(string keyword, string skillDescription);

        /// <summary>
        /// Removes a canonical tag by changing it to no longer be canonical.
        /// </summary>
        /// <param name="tagId">The id of the canonical tag to remove.</param>
        Task RemoveCanonicalTagAsync(int tagId);

        /// <summary>
        /// Updates a tag.
        /// </summary>
        /// <param name="tagId">The id of the tag to update.</param>
        /// <param name="keyword">The new keyword for the tag.</param>
        /// <param name="skillDescription">The new skill description for the tag.</param>
        /// <param name="isCanonical">Whether or not the tag is canonical.</param>
        Task UpdateTagAsync(int tagId, string keyword, string skillDescription, bool isCanonical);

        /// <summary>
        /// Loads a tag by its keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>The tag with the given keyword.</returns>
        /// <remarks>
        /// There is a unique index on keyword so only one tag will be returned.
        /// </remarks>
        Task<Tag> LoadByKeywordAsync(string keyword);

        /// <summary>
        /// Loads a tag by its keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>The tag with the given keyword.</returns>
        /// <remarks>
        /// There is a unique index on keyword so only one tag will be returned.
        /// </remarks>
        Tag LoadByKeyword(string keyword);
    }
}
