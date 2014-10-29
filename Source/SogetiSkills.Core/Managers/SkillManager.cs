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
        public async Task<IEnumerable<ConsultantSkill>> LoadSkillsForConsultantAsync(int consultantId)
        {
            var command = new SqlCommand("Skill_SelectByConsultantId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consultantId", consultantId);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadConsultantSkillsAsync(reader);
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
        /// <param name="description">An optional skill description.</param>
        public async Task AddCanonicalSkillAsync(string name)
        {
            var tag = await LoadByNameAsync(name);
            if (tag != null)
            {
                await UpdateSkillAsync(tag.Id, name, true);
            }
            else
            {
                var command = new SqlCommand("Skill_InsertCanonical", await GetOpenConnectionAsync());
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", name);

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
        public async Task UpdateSkillAsync(int skillId, string name, bool isCanonical)
        {
            var command = new SqlCommand("Skill_Update", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", skillId);
            command.Parameters.AddWithValue("@name", name);
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

        /// <summary>
        /// Adds a skill to a consultant.  If the skill does not already exist then it is inserted.
        /// </summary>
        /// <param name="skillName">The name of the skill to add.</param>
        /// <param name="consultantId">The id of the consultant to add the skill to.</param>
        /// <param name="proficiency">The proficiency of the consultant with the skill.</param>
        /// <returns>The skill that was added to the consultant.</returns>
        public async Task<ConsultantSkill> AddSkillToConsultantAsync(string skillName, int consultantId, int proficiencyLevel)
        {
            var skill = await LoadByNameAsync(skillName);

            SqlCommand command = null;
            if (skill == null)
            {
                // If the skill doesn't exist yet then insert and tie it to the consultant.
                command = new SqlCommand("Skill_InsertNonCanonicalForConsultant", await GetOpenConnectionAsync());
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", skillName);
                command.Parameters.AddWithValue("@consultantId", consultantId);
                command.Parameters.AddWithValue("@proficiencyLevel", proficiencyLevel);
                int skillId = (int)(await command.ExecuteScalarAsync());
                skill = new Skill
                {
                    Id = skillId,
                    Name = skillName,
                    IsCanonical = false
                };
            }
            else
            {
                // Else the skill does exist then just tie it to the consultant.
                command = new SqlCommand("Skill_AddToConsultant", await GetOpenConnectionAsync());
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@skillId", skill.Id);
                command.Parameters.AddWithValue("@consultantId", consultantId);
                command.Parameters.AddWithValue("@proficiencyLevel", proficiencyLevel);
                await command.ExecuteNonQueryAsync();
            }

            // Select and return the joining table between the consultant and skill.
            command = new SqlCommand("Skill_SelectConsultantSkill", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@skillId", skill.Id);
            command.Parameters.AddWithValue("@consultantId", consultantId);
            using(SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    return ReadConsultantSkill(reader);
                }
            }

            return null;
        }

        /// <summary>
        /// Removes a skill from a consultant.  If the skill is not marked as canonical and is not used by
        /// any other consultants than the skill itself is deleted.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to remove the skill from.</param>
        /// <param name="skillId">The id of the skill to remove from the consultant.</param>
        public async Task RemoveSkillFromConsultantAsync(int consultantId, int skillId)
        {
            var command = new SqlCommand("Skill_RemoveFromConsultant", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consultantId", consultantId);
            command.Parameters.AddWithValue("@skillId", skillId);
            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Loads all proficiency levels ordered by level.
        /// </summary>
        /// <returns>All proficiency levels.</returns>
        public async Task<IEnumerable<ProficiencyLevel>> LoadProficiencyLevelsAsync()
        {
            var command = new SqlCommand("ProficiencyLevels_SelectAll", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            List<ProficiencyLevel> proficiencyLevels = new List<ProficiencyLevel>();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    var proficiencyLevel = new ProficiencyLevel();
                    proficiencyLevel.Level = reader.Field<int>("Level");
                    proficiencyLevel.Name = reader.Field<string>("Name");
                    proficiencyLevel.SecondPersonDescription = reader.Field<string>("SecondPersonDescription");
                    proficiencyLevel.ThirdPersonDescription = reader.Field<string>("ThirdPersonDescription");
                    proficiencyLevels.Add(proficiencyLevel);
                }
            }
            return proficiencyLevels;
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
            skill.IsCanonical = reader.Field<bool>("IsCanonical");
            return skill;
        }

        private async Task<IEnumerable<ConsultantSkill>> ReadConsultantSkillsAsync(SqlDataReader reader)
        {
            List<ConsultantSkill> consultantSkills = new List<ConsultantSkill>();
            while (await reader.ReadAsync())
            {
                consultantSkills.Add(ReadConsultantSkill(reader));
            }
            return consultantSkills;
        }

        private ConsultantSkill ReadConsultantSkill(SqlDataReader reader)
        {
            ConsultantSkill consultantSkill = new ConsultantSkill();
            consultantSkill.ConsultantId = reader.Field<int>("ConsultantId");
            consultantSkill.SkillId = reader.Field<int>("SkillId");
            consultantSkill.SkillName = reader.Field<string>("SkillName");
            consultantSkill.IsCanonical = reader.Field<bool>("IsCanonical");
            consultantSkill.Proficiency = new ProficiencyLevel
            {
                Level = reader.Field<int>("ProficiencyLevel"),
                Name = reader.Field<string>("ProficiencyLevelName")
            };
            return consultantSkill;
        }
        #endregion
    }
}