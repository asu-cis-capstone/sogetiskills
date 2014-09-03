namespace SogetiSkills.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkillsAndBasicProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Profile_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Username)
                .Index(t => t.Profile_Username);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "Profile_Username", "dbo.Profiles");
            DropForeignKey("dbo.Skill_SkillCategory", "SkillCategoryId", "dbo.SkillCategories");
            DropForeignKey("dbo.Skill_SkillCategory", "SkillId", "dbo.Skills");
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillCategoryId" });
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillId" });
            DropIndex("dbo.Skills", new[] { "Profile_Username" });
            DropTable("dbo.Skill_SkillCategory");
            DropTable("dbo.Profiles");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillCategories");
        }
    }
}
