namespace SogetiSkills.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkillsCategoriesAndProfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 450),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Bio = c.String(),
                    })
                .Index(x => x.Username)
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 450),
                    })
                .Index(x => x.Name)
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 450),
                    })
                 .Index(x => x.Name)
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skill_SkillCategory",
                c => new
                    {
                        SkillId = c.Int(nullable: false),
                        SkillCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillId, t.SkillCategoryId })
                .ForeignKey("dbo.Skills", t => t.SkillId)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategoryId)
                .Index(t => t.SkillId)
                .Index(t => t.SkillCategoryId);
            
            CreateTable(
                "dbo.Skill_Profile",
                c => new
                    {
                        SkillId = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SkillId, t.ProfileId })
                .ForeignKey("dbo.Skills", t => t.SkillId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.SkillId)
                .Index(t => t.ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skill_Profile", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Skill_Profile", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Skill_SkillCategory", "SkillCategoryId", "dbo.SkillCategories");
            DropForeignKey("dbo.Skill_SkillCategory", "SkillId", "dbo.Skills");
            DropIndex("dbo.Skill_Profile", new[] { "ProfileId" });
            DropIndex("dbo.Skill_Profile", new[] { "SkillId" });
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillCategoryId" });
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillId" });
            DropTable("dbo.Skill_Profile");
            DropTable("dbo.Skill_SkillCategory");
            DropTable("dbo.SkillCategories");
            DropTable("dbo.Skills");
            DropTable("dbo.Profiles");
        }
    }
}
