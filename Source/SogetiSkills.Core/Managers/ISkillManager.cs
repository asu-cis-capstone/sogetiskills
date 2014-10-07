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
        Task<IEnumerable<Skill>> LoadSkillsForConsultantAsync(int consultantId);

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
        /// <param name="skillDescrisption">An optional skill description.</param>
        Task AddCanonicalSkillAsync(string keyword, string skillDescription);

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
        Task UpdateSkillAsync(int skillId, string name, string skillDescription, bool isCanonical);

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
    }
}
