using System;
using System.Data.Entity.Migrations;

namespace SogetiSkills.Migrations
{   
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileData = c.Binary(),
                        FileName = c.String(),
                        MimeType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Keyword = c.String(),
                        SkillDescription = c.String(),
                        IsCanonical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Password_Hash = c.String(),
                        Password_Salt = c.String(),
                        IsOnBeach = c.Boolean(),
                        ResumeId = c.Int(),
                        UserType = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Resumes", t => t.ResumeId)
                .Index(t => t.ResumeId);
            
            CreateTable(
                "Consultant_Tag",
                c => new
                    {
                        ConsultantId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConsultantId, t.TagId })
                .ForeignKey("Users", t => t.ConsultantId)
                .ForeignKey("Tags", t => t.TagId)
                .Index(t => t.ConsultantId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Consultant_Tag", "TagId", "Tags");
            DropForeignKey("Consultant_Tag", "ConsultantId", "Users");
            DropForeignKey("Users", "ResumeId", "Resumes");
            DropIndex("Consultant_Tag", new[] { "TagId" });
            DropIndex("Consultant_Tag", new[] { "ConsultantId" });
            DropIndex("Users", new[] { "ResumeId" });
            DropTable("Consultant_Tag");
            DropTable("Users");
            DropTable("Tags");
            DropTable("Resumes");
        }
    }
}
