namespace SogetiSkills.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillsAndSkillCategories : DbMigration
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
                    })
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skill_SkillCategory", "SkillCategoryId", "dbo.SkillCategories");
            DropForeignKey("dbo.Skill_SkillCategory", "SkillId", "dbo.Skills");
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillCategoryId" });
            DropIndex("dbo.Skill_SkillCategory", new[] { "SkillId" });
            DropTable("dbo.Skill_SkillCategory");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillCategories");
        }
    }
}
