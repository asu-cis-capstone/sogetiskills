using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SogetiSkills.API.Models
{
    public class SogetiSkillsDataContext : DbContext
    {
        public SogetiSkillsDataContext()
            : base("name=SogetiSkills")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<SkillCategory>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Skill>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Skill>().HasMany(x => x.Categorites).WithMany(x => x.Skills).Map(x =>
                {
                    x.ToTable("Skill_SkillCategory");
                    x.MapLeftKey("SkillId");
                    x.MapRightKey("SkillCategoryId");
                });
        }
        
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}