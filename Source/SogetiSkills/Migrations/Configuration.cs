using SogetiSkills.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SogetiSkills.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SogetiSkillsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }
    }
}
