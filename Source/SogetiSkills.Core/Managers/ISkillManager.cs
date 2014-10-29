using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    /// <summary>
    /// Provides data access for skills.
    /// </summary>
    public interface ISkillManager
    {
        /// <summary>
        /// Load all of the skills that have been applied to a consultant.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to load skills for.</param>
        /// <returns>All the skills that have been applied to the consultant</returns>
        Task<IEnumerable<ConsultantSkill>> LoadSkillsForConsultantAsync(int consultantId);

        /// <summary>
        /// Load all of the canonical skills from the database.  Account executives maintain the list
        /// of canonical skills.
        /// </summary>
        /// <returns>All canonical skills ordered by name.</returns>
        Task<IEnumerable<Skill>> LoadCanonicalSkillsAsync();

        /// <summary>
        /// Inserts a new canonical skill.
        /// </summary>
        /// <param name="name">The skills's name.</param>
        /// <param name="description">An optional skill description.</param>
        Task AddCanonicalSkillAsync(string name);

        /// <summary>
        /// Removes a canonical skill.  If the skill has been used by a consultant then it is flagged as
        /// no longer canonical.  If the skill has not been used by any consultants then it is actually
        /// deleted from the database.
        /// </summary>
        /// <param name="skillId">The id of the canonical skill to remove.</param>
        Task RemoveCanonicalSkillAsync(int skillId);

        /// <summary>
        /// Updates a skill.
        /// </summary>
        /// <param name="skillId">The id of the skill to update.</param>
        /// <param name="name">The new name for the skill.</param>
        /// <param name="skillDescription">The new description for the skill.</param>
        /// <param name="isCanonical">Whether or not the skill is canonical.</param>
        Task UpdateSkillAsync(int skillId, string name, bool isCanonical);

        /// <summary>
        /// Loads a skill by its name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The skill with the given name.</returns>
        /// <remarks>
        /// There is a unique index on name so only one skill will be returned.
        /// </remarks>
        Task<Skill> LoadByNameAsync(string name);

        /// <summary>
        /// Loads a skill by its name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The skill with the given name.</returns>
        /// <remarks>
        /// There is a unique index on name so only one skill will be returned.
        /// </remarks>
        Skill LoadByName(string name);

        /// <summary>
        /// Loads a skill by its id.
        /// </summary>
        /// <param name="id">The id of the skill to load.</param>
        /// <returns>The skill with the given id.</returns>
        Task<Skill> LoadByIdAsync(int id);

        /// <summary>
        /// Loads a skill by its id.
        /// </summary>
        /// <param name="id">The id of the skill to load.</param>
        /// <returns>The skill with the given id.</returns>
        Skill LoadById(int id);

        /// <summary>
        /// Adds a skill to a consultant.  If the skill does not already exist then it is inserted.
        /// </summary>
        /// <param name="skillName">The name of the skill to add.</param>
        /// <param name="consultantId">The id of the consultant to add the skill to.</param>
        /// <param name="proficiencyLevel">The proficiency of the consultant with the skill.</param>
        /// <returns>The skill that was added to the consultant.</returns>
        Task<ConsultantSkill> AddSkillToConsultantAsync(string skillName, int consultantId, int proficiencyLevel);

        /// <summary>
        /// Removes a skill from a consultant.  If the skill is not marked as canonical and is not used by
        /// any other consultants than the skill itself is deleted.
        /// </summary>
        /// <param name="consultantId">The id of the consultant to remove the skill from.</param>
        /// <param name="skillId">The id of the skill to remove from the consultant.</param>
        Task RemoveSkillFromConsultantAsync(int consultantId, int skillId);

        /// <summary>
        /// Loads all proficiency levels ordered by level.
        /// </summary>
        /// <returns>All proficiency levels.</returns>
        Task<IEnumerable<ProficiencyLevel>> LoadProficiencyLevelsAsync();
    }
}
