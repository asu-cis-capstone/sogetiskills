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

            modelBuilder.Entity<SkillCategory>().Property(x => x.Name).IsRequired().HasMaxLength(450);
            modelBuilder.Entity<Skill>().Property(x => x.Name).IsRequired().HasMaxLength(450);
            modelBuilder.Entity<Skill>().HasMany(x => x.Categorites).WithMany(x => x.Skills).Map(x =>
                {
                    x.ToTable("Skill_SkillCategory");
                    x.MapLeftKey("SkillId");
                    x.MapRightKey("SkillCategoryId");
                });
            modelBuilder.Entity<Skill>().HasMany(x => x.Profiles).WithMany(x => x.Skills).Map(x =>
            {
                x.ToTable("Skill_Profile");
                x.MapLeftKey("SkillId");
                x.MapRightKey("ProfileId");
            });

            modelBuilder.Entity<Profile>().Property(x => x.Username).IsRequired().HasMaxLength(450);
        }

        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<SkillCategory> SkillCategories { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
    }
}