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
    /// Provides data access for skills.
    /// </summary>
    public class SkillManager : ManagerBase, ISkillManager
    {
        /// <summary>
        /// Load all of the skills that have been applied to a consultant.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to load skills for.</param>
        /// <returns>All the skills that have been applied to the consultant</returns>
        public async Task<IEnumerable<Skill>> LoadSkillsForConsultantAsync(int consultantId)
        {
            var command = new SqlCommand("Skill_SelectByConsultantId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consultantId", consultantId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadSkillsAsync(reader);
            }
        }

        /// <summary>
        /// Load all of the canonical skills from the database.  Account executives maintain the list
        /// of canonical skills.
        /// </summary>
        /// <returns>All canonical skills ordered by name.</returns>
        public async Task<IEnumerable<Skill>> LoadCanonicalSkillsAsync()
        {
            var command = new SqlCommand("Skill_SelectCanonical", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadSkillsAsync(reader);
            }
        }

        /// <summary>
        /// Inserts a new canonical skill.
        /// </summary>
        /// <param name="name">The skills's name.</param>
        /// <param name="skillDescription">An optional skill description.</param>
        public async Task AddCanonicalSkillAsync(string name, string skillDescription)
        {
            var tag = await LoadByNameAsync(name);
            if (tag != null)
            {
                await UpdateSkillAsync(tag.Id, name, skillDescription, true);
            }
            else
            {
                var command = new SqlCommand("Skill_InsertCanonical", await GetOpenConnectionAsync());
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", DataAccessHelper.ValueOrDBNull(skillDescription));

                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Removes a canonical skill.  If the skill has been used by a consultant then it is flagged as
        /// no longer canonical.  If the skill has not been used by any consultants then it is actually
        /// deleted from the database.
        /// </summary>
        /// <param name="skillId">The id of the canonical skill to remove.</param>
        public async Task RemoveCanonicalSkillAsync(int skillId)
        {
            var command = new SqlCommand("Skill_DeleteCanonical", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", skillId);

            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Updates a skill.
        /// </summary>
        /// <param name="skillId">The id of the skill to update.</param>
        /// <param name="name">The new name for the skill.</param>
        /// <param name="skillDescription">The new description for the skill.</param>
        /// <param name="isCanonical">Whether or not the skill is canonical.</param>
        public async Task UpdateSkillAsync(int skillId, string name, string skillDescription, bool isCanonical)
        {
            var command = new SqlCommand("Skill_Update", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", skillId);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", DataAccessHelper.ValueOrDBNull(skillDescription));
            command.Parameters.AddWithValue("@isCanonical", isCanonical);

            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Loads a skill by its name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The skill with the given name.</returns>
        /// <remarks>
        /// There is a unique index on name so only one skill will be returned.
        /// </remarks>
        public async Task<Skill> LoadByNameAsync(string name)
        {
            var command = new SqlCommand("Skill_SelectByName", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", name);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync())
                {
                    return ReadSkill(reader);
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a skill by its name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The skill with the given name.</returns>
        /// <remarks>
        /// There is a unique index on name so only one skill will be returned.
        /// </remarks>
        public Skill LoadByName(string name)
        {
            var command = new SqlCommand("Skill_SelectByName", GetOpenConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", name);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ReadSkill(reader);
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a skill by its id.
        /// </summary>
        /// <param name="id">The id of the skill to load.</param>
        /// <returns>The skill with the given id.</returns>
        public async Task<Skill> LoadByIdAsync(int id)
        {
            var command = new SqlCommand("Skill_SelectById", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return ReadSkill(reader);
                }
            }
            return null;
        }

        /// <summary>
        /// Loads a skill by its id.
        /// </summary>
        /// <param name="id">The id of the skill to load.</param>
        /// <returns>The skill with the given id.</returns>
        public Skill LoadById(int id)
        {
            var command = new SqlCommand("Skill_SelectById", GetOpenConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ReadSkill(reader);
                }
            }
            return null;
        }

        #region Private helper methods
        // Helper methods for creating skill objects from a data reader.
        private async Task<IEnumerable<Skill>> ReadSkillsAsync(SqlDataReader reader)
        {
            List<Skill> skills = new List<Skill>();
            while(await reader.ReadAsync())
            {
                skills.Add(ReadSkill(reader));
            }
            return skills;
        }

        private Skill ReadSkill(SqlDataReader reader)
        {
            Skill skill = new Skill();
            skill.Id = reader.Field<int>("Id");
            skill.Name = reader.Field<string>("Name");
            skill.Description = reader.Field<string>("Description");
            skill.IsCanonical = reader.Field<bool>("IsCanonical");
            return skill;
        }
        #endregion
    }
}