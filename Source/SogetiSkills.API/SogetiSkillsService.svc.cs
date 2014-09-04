using SogetiSkills.API.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using AutoMapper;
using Seterlund.CodeGuard;

namespace SogetiSkills.API
{    
    public class SogetiSkillsService : ISogetiSkillsService, IDisposable
    {
        private readonly Models.SogetiSkillsDataContext _db;

        public SogetiSkillsService(Models.SogetiSkillsDataContext db)
        {
            _db = db;
        }

        public string GetVersion()
        {
            return string.Format("{0} ({1})", AppSettings.ApplicationVersion, AppSettings.ApplicationReleaseProfile);
        }

        public void Skill_AddCateogry(string name)
        {
            Guard.That(name).IsNotNullOrWhiteSpace();

            var skillCategory = _db.SkillCategories.FirstOrDefault(x => x.Name == name);
            if (skillCategory == null)
            {
                skillCategory = _db.SkillCategories.Create();
                skillCategory.Name = name;

                _db.SkillCategories.Add(skillCategory);
                _db.SaveChanges();
            }
        }

        public void Skill_AddSkill(string category, string name)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                category = "Misc";
            }

            var skillCategory = _db.SkillCategories.Include(x => x.Skills).FirstOrDefault(x => x.Name == category);
            Guard.That(skillCategory).IsNotNull();

            if (!skillCategory.Skills.Any(x => x.Name == name))
            {
                var skill = _db.Skills.FirstOrDefault(x => x.Name == name);
                if (skill == null)
                {
                    skill = new Models.Skill { Name = name };
                    _db.Skills.Add(skill);
                }
                skillCategory.Skills.Add(skill);

                _db.SaveChanges();
            }
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
