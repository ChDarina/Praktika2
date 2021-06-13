namespace Praktika2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        CustomerEMail = c.String(nullable: false, maxLength: 255),
                        CustomerNickname = c.String(nullable: false, maxLength: 255),
                        CustomerPassword = c.String(nullable: false, maxLength: 255),
                        CustomerPhone = c.Long(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        IllustratorID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        IllustrationID = c.Int(),
                        Commentary = c.String(),
                        Feedback = c.String(),
                        OrderStatus = c.String(maxLength: 255),
                        Price = c.Int(),
                        OrderDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Illustrators", t => t.IllustratorID, cascadeDelete: true)
                .ForeignKey("dbo.Illustrations", t => t.IllustrationID)
                .Index(t => t.IllustratorID)
                .Index(t => t.CustomerID)
                .Index(t => t.IllustrationID);
            
            CreateTable(
                "dbo.Illustrations",
                c => new
                    {
                        IllustrationID = c.Int(nullable: false, identity: true),
                        IllustratorID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        DLDate = c.DateTime(nullable: false),
                        Privacy = c.Boolean(nullable: false),
                        Hiding = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IllustrationID)
                .ForeignKey("dbo.Illustrators", t => t.IllustratorID, cascadeDelete: true)
                .Index(t => t.IllustratorID);
            
            CreateTable(
                "dbo.Illustrators",
                c => new
                    {
                        IllustratorID = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        IllustratorEMail = c.String(nullable: false, maxLength: 255),
                        IllustratorNickname = c.String(nullable: false, maxLength: 255),
                        IllustratorPassword = c.String(nullable: false, maxLength: 255),
                        IllustratorPhone = c.Long(),
                    })
                .PrimaryKey(t => t.IllustratorID);
            
            CreateTable(
                "dbo.SocialMediaIllustrators",
                c => new
                    {
                        IllustratorID = c.Int(nullable: false),
                        SocialMediaID = c.Int(nullable: false),
                        IllustratorReference = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.IllustratorID, t.SocialMediaID })
                .ForeignKey("dbo.Illustrators", t => t.IllustratorID, cascadeDelete: true)
                .ForeignKey("dbo.SocialMedias", t => t.SocialMediaID, cascadeDelete: true)
                .Index(t => t.IllustratorID)
                .Index(t => t.SocialMediaID);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        SocialMediaID = c.Int(nullable: false, identity: true),
                        SocialMediaName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.SocialMediaID);
            
            CreateTable(
                "dbo.SocialMediaCustomers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        SocialMediaID = c.Int(nullable: false),
                        SocialMediaReference = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.SocialMediaID })
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.SocialMedias", t => t.SocialMediaID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.SocialMediaID);
            
            CreateTable(
                "dbo.Technics",
                c => new
                    {
                        TechnicID = c.Int(nullable: false, identity: true),
                        TechnicName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TechnicID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        diagram_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => new { t.name, t.principal_id, t.diagram_id });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TechnicsIllustrations",
                c => new
                    {
                        Technics_TechnicID = c.Int(nullable: false),
                        Illustrations_IllustrationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Technics_TechnicID, t.Illustrations_IllustrationID })
                .ForeignKey("dbo.Technics", t => t.Technics_TechnicID, cascadeDelete: true)
                .ForeignKey("dbo.Illustrations", t => t.Illustrations_IllustrationID, cascadeDelete: true)
                .Index(t => t.Technics_TechnicID)
                .Index(t => t.Illustrations_IllustrationID);
            
            CreateTable(
                "dbo.TechnicsIllustrators",
                c => new
                    {
                        Technics_TechnicID = c.Int(nullable: false),
                        Illustrators_IllustratorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Technics_TechnicID, t.Illustrators_IllustratorID })
                .ForeignKey("dbo.Technics", t => t.Technics_TechnicID, cascadeDelete: true)
                .ForeignKey("dbo.Illustrators", t => t.Illustrators_IllustratorID, cascadeDelete: true)
                .Index(t => t.Technics_TechnicID)
                .Index(t => t.Illustrators_IllustratorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "IllustrationID", "dbo.Illustrations");
            DropForeignKey("dbo.TechnicsIllustrators", "Illustrators_IllustratorID", "dbo.Illustrators");
            DropForeignKey("dbo.TechnicsIllustrators", "Technics_TechnicID", "dbo.Technics");
            DropForeignKey("dbo.TechnicsIllustrations", "Illustrations_IllustrationID", "dbo.Illustrations");
            DropForeignKey("dbo.TechnicsIllustrations", "Technics_TechnicID", "dbo.Technics");
            DropForeignKey("dbo.SocialMediaIllustrators", "SocialMediaID", "dbo.SocialMedias");
            DropForeignKey("dbo.SocialMediaCustomers", "SocialMediaID", "dbo.SocialMedias");
            DropForeignKey("dbo.SocialMediaCustomers", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.SocialMediaIllustrators", "IllustratorID", "dbo.Illustrators");
            DropForeignKey("dbo.Orders", "IllustratorID", "dbo.Illustrators");
            DropForeignKey("dbo.Illustrations", "IllustratorID", "dbo.Illustrators");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.TechnicsIllustrators", new[] { "Illustrators_IllustratorID" });
            DropIndex("dbo.TechnicsIllustrators", new[] { "Technics_TechnicID" });
            DropIndex("dbo.TechnicsIllustrations", new[] { "Illustrations_IllustrationID" });
            DropIndex("dbo.TechnicsIllustrations", new[] { "Technics_TechnicID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SocialMediaCustomers", new[] { "SocialMediaID" });
            DropIndex("dbo.SocialMediaCustomers", new[] { "CustomerID" });
            DropIndex("dbo.SocialMediaIllustrators", new[] { "SocialMediaID" });
            DropIndex("dbo.SocialMediaIllustrators", new[] { "IllustratorID" });
            DropIndex("dbo.Illustrations", new[] { "IllustratorID" });
            DropIndex("dbo.Orders", new[] { "IllustrationID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "IllustratorID" });
            DropTable("dbo.TechnicsIllustrators");
            DropTable("dbo.TechnicsIllustrations");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Technics");
            DropTable("dbo.SocialMediaCustomers");
            DropTable("dbo.SocialMedias");
            DropTable("dbo.SocialMediaIllustrators");
            DropTable("dbo.Illustrators");
            DropTable("dbo.Illustrations");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
