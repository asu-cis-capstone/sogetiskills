using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SogetiSkills.Core.Helpers;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for tags (skills).
    /// </summary>
    public class TagManager : ManagerBase, ITagManager
    {
        /// <summary>
        /// Load all of the tags that have been applied to a consultant.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to load tags for.</param>
        /// <returns>All the tags that have been applied to consultant</returns>
        public async Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(int consultantId)
        {
            var command = new SqlCommand("Tag_SelectByConsultantId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consultantId", consultantId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadTagRowsAsync(reader);
            }
        }

        /// <summary>
        /// Load all of the canonical tags from the database.  Account executives maintain the list
        /// of canonical tags.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tag>> LoadCanonicalTagsAsync()
        {
            var command = new SqlCommand("Tag_SelectCanonical", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadTagRowsAsync(reader);
            }
        }

        /// <summary>
        /// Inserts a new canonical tag.
        /// </summary>
        /// <param name="keyword">The tag's keyword.</param>
        /// <param name="skillDescription">An optional tag description.</param>
        public async Task AddCanonicalTagAsync(string keyword, string skillDescription)
        {
            var tag = await LoadByKeywordAsync(keyword);
            if (tag != null)
            {
                await UpdateTagAsync(tag.Id, keyword, skillDescription, true);
            }
            else
            {
                var command = new SqlCommand("Tag_InsertCanonical", await GetOpenConnectionAsync());
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@keyword", keyword);
                command.Parameters.AddWithValue("@skillDescription", skillDescription);

                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Removes a canonical tag by changing it to no longer be canonical.
        /// </summary>
        /// <param name="tagId">The id of the canonical tag to remove.</param>
        public async Task RemoveCanonicalTagAsync(int tagId)
        {
            var command = new SqlCommand("Tag_DeleteCanonical", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", tagId);

            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Updates a tag.
        /// </summary>
        /// <param name="tagId">The id of the tag to update.</param>
        /// <param name="keyword">The new keyword for the tag.</param>
        /// /// <param name="skillDescription">The new skill description for the tag.</param>
        /// /// <param name="isCanonical">Whether or not the tag is canonical.</param>
        public async Task UpdateTagAsync(int tagId, string keyword, string skillDescription, bool isCanonical)
        {
            var command = new SqlCommand("Tag_Update", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", tagId);
            command.Parameters.AddWithValue("@keyword", keyword);
            command.Parameters.AddWithValue("@skillDescription", skillDescription);
            command.Parameters.AddWithValue("@isCanonical", isCanonical);

            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Loads a tag by its keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>The tag with the given keyword.</returns>
        /// <remarks>
        /// There is a unique index on keyword so only one tag will be returned.
        /// </remarks>
        public async Task<Tag> LoadByKeywordAsync(string keyword)
        {
            var command = new SqlCommand("Tag_SelectByKeyword", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@keyword", keyword);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync())
                {
                    return ReadTagRow(reader);
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a tag by its keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>The tag with the given keyword.</returns>
        /// <remarks>
        /// There is a unique index on keyword so only one tag will be returned.
        /// </remarks>
        public Tag LoadByKeyword(string keyword)
        {
            var command = new SqlCommand("Tag_SelectByKeyword", GetOpenConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@keyword", keyword);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ReadTagRow(reader);
                }
            }
            return null;
        }

        #region Private helper methods
        // Helper methods for creating tag objects from a data reader.
        private async Task<IEnumerable<Tag>> ReadTagRowsAsync(SqlDataReader reader)
        {
            List<Tag> tags = new List<Tag>();
            while(await reader.ReadAsync())
            {
                tags.Add(ReadTagRow(reader));
            }
            return tags;
        }

        private Tag ReadTagRow(SqlDataReader reader)
        {
            Tag tag = new Tag();
            tag.Id = reader.Field<int>("Id");
            tag.Keyword = reader.Field<string>("Keyword");
            tag.SkillDescription = reader.Field<string>("SkillDescription");
            tag.IsCanonical = reader.Field<bool>("IsCanonical");
            return tag;
        }
        #endregion
    }
}