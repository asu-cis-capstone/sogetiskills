using SogetiSkills.API.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using AutoMapper;

namespace SogetiSkills.API
{    
    public class SogetiSkillsService : ISogetiSkillsService, IDisposable
    {
        private readonly Models.SogetiSkillsDataContext _db;

        public SogetiSkillsService(Models.SogetiSkillsDataContext db)
        {
            _db = db;
        }

        public IEnumerable<SkillCategory> Skills_GetAll()
        {
            var skillCategories = _db.SkillCategories.Include(x => x.Skills).ToList();
            return Mapper.Map<IEnumerable<SkillCategory>>(skillCategories);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
