using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Models
{
    public class SogetiSkillsDataContext : DbContext
    {
        public SogetiSkillsDataContext()
            : base ("Name=SogetiSkills")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Users are either a Consultant or an AccountExecutite.  All users have a hashed password so the
            // Users table is split between the User and HashedPassword entity.
            modelBuilder.Entity<User>()
                .HasRequired(x => x.Password)
                .WithMany()
                .HasForeignKey(x => x.PasswordId);
            modelBuilder.Entity<User>()
                .Map<AccountExecutive>(x => x.Requires("UserType").HasValue("AccountExecutive"))
                .Map<Consultant>(x => x.Requires("UserType").HasValue("Consultant"));
         
            // Consultants have a resume.
            modelBuilder.Entity<Consultant>()
                .HasOptional(x => x.Resume)
                .WithMany()
                .HasForeignKey(x => x.ResumeId);

            // There is a many-to-many relationship between consultants and tags.
            modelBuilder.Entity<Consultant>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Consultants)
                .Map(x =>
                {
                    x.ToTable("Consultant_Tag");
                    x.MapLeftKey("ConsultantId");
                    x.MapRightKey("TagId");
                });

            modelBuilder.Entity<HashedPassword>().ToTable("Passwords");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<HashedPassword> Passwords { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
