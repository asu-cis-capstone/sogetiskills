namespace SogetiSkills.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SogetiSkills.API.Models.SogetiSkillsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SogetiSkills.API.Models.SogetiSkillsDataContext context)
        {
            
        }
    }
}
