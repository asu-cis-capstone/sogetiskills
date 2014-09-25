using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SogetiSkills.Helpers;

namespace SogetiSkills.Managers
{
    public class TagManager : ManagerBase, ITagManager
    {
        public async Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(int consultantId)
        {
            var command = new SqlCommand("Tag_SelectByConsultantId", await GetOpenConnectionAsync());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consultantId", consultantId);

            List<Tag> tags = new List<Tag>();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Tag tag = new Tag();
                    tag.Id = reader.Field<int>("Id");
                    tag.Keyword = reader.Field<string>("Keyword");
                    tag.SkillDescription = reader.Field<string>("SkillDescription");
                    tag.IsCanonical = reader.Field<bool>("IsCanonical");
                    tags.Add(tag);
                }
            }
            return tags;
        }
    }
}
