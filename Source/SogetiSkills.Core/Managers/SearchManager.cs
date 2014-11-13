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
    /// Allows searching for consultants.
    /// </summary>
    public class SearchManager : ManagerBase, ISearchManager
    {
        /// <summary>
        /// Get all consultants and filter the results.
        /// </summary>
        /// <param name="beachStatus">Whether or not the consultant is currently on the beach.  Pass null to ignore the beach status.</param>
        /// <param name="lastName">Filter by the consultant's last name.  The comparison is case insensitive and filters where the consultant's last name contains the string.  Pass null to ignore the last name.</param>
        /// <param name="emailAddress">Filter by the consultant's email.  The comparison is case insensitive and but requires an exact match.  Pass null to ignore the email address.</param>
        /// <param name="skills">A list of skills to filter by.  The comparisons are case insensitive and a consultant only needs to match one skill to be included in the result set.</param>
        /// <returns>A filtered list of consultants.</returns>
        public async Task<IEnumerable<ConsultantWithSkills>> SearchConsultantsAsync(bool? beachStatus, string lastName, string emailAddress, IEnumerable<string> skills)
        {
            // For now it will be fast enough to just pull back all consultants and their skills and then do the
            // filter in memory.  It would be nice to filter in the database but we gain a lot of maintainability
            // by using linq instead of dynamically building SQL inside of a stored procedure and I think the trade
            // off is worth it.

            var query = (await GetAllConsultantsAsync()).AsQueryable();
            if (beachStatus.HasValue)
            {
                query = query.Where(x => x.IsOnBeach == beachStatus);
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                query = query.Where(x => x.EmailAddress.ToLower().Contains(emailAddress.ToLower()));
            }

            if (skills != null && skills.Any())
            {
                var loweredSkills = skills
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.ToLower())
                    .ToList();

                query = from x in query
                        where x.Skills.Select(s => s.SkillName.ToLower()).Intersect(loweredSkills).Any()
                        select x;
            }
            return query.OrderBy(x => x.LastName).ToList();
        }

        #region Private helper method
        // Go the the database and pull back all consultants with their skills.
        private async Task<IEnumerable<ConsultantWithSkills>> GetAllConsultantsAsync()
        {
            var command = new SqlCommand("Consultant_SelectAllWithSkills", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;            

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                return await ReadConsultantsAndSkillsAsync(reader);
            }
        }
        
        // Read back both results sets and create a single collection of consultants with their skills.
        private async Task<IEnumerable<ConsultantWithSkills>> ReadConsultantsAndSkillsAsync(SqlDataReader reader)
        {
            var consultants = new Dictionary<int, ConsultantWithSkills>();

            // Read all of the consultants themselves.
            while(await reader.ReadAsync())
            {
                var consultant = new ConsultantWithSkills();
                consultant.IsOnBeach = reader.Field<bool>("IsOnBeach");
                consultant.Id = reader.Field<int>("Id");
                consultant.EmailAddress = reader.Field<string>("EmailAddress");
                consultant.FirstName = reader.Field<string>("FirstName");
                consultant.LastName = reader.Field<string>("LastName");
                consultant.PhoneNumber = new PhoneNumber(reader.Field<string>("PhoneNumber"));
                consultants.Add(consultant.Id, consultant);
            }

            // Advance to the next result set and read the consultant skills.
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    var consultantSkill = new ConsultantSkill();
                    consultantSkill.ConsultantId = reader.Field<int>("ConsultantId");
                    consultantSkill.SkillId = reader.Field<int>("SkillId");
                    consultantSkill.SkillName = reader.Field<string>("SkillName");
                    consultantSkill.IsCanonical = reader.Field<bool>("IsCanonical");
                    consultantSkill.Proficiency = new ProficiencyLevel
                    {
                        Level = reader.Field<int>("ProficiencyLevel"),
                        Name = reader.Field<string>("ProficiencyLevelName")
                    };
                    var consultant = consultants[consultantSkill.ConsultantId];
                    consultant.Skills.Add(consultantSkill);
                }
            }

            return consultants.Select(x => x.Value).ToList();
        }
        #endregion
    }
}
