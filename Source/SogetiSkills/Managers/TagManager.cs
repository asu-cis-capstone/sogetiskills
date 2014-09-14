using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SogetiSkills.Managers
{
    public class TagManager : ITagManager
    {
        private readonly SogetiSkillsDataContext _db;

        public TagManager(SogetiSkillsDataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Tag>> LoadTagsForConsultantAsync(int consultantId)
        {
            return await (from x in _db.Users.OfType<Consultant>()
                          where x.Id == consultantId
                          select x.Tags).FirstAsync();
        }
    }
}
