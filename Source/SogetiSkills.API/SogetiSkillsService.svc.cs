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

        public Contracts.DataContracts.Profile Profile_GetByUsername(string username)
        {
            var profile = _db.Profiles.Include(x => x.Skills).FirstOrDefault(x => x.Username == username);
            if (profile == null)
            {
                profile = new Models.Profile { Username = username };
                _db.Profiles.Add(profile);
                _db.SaveChanges();
            }
            return Mapper.Map<Contracts.DataContracts.Profile>(profile);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
