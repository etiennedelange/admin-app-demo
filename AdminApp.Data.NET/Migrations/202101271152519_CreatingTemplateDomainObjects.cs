namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingTemplateDomainObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesktopProductVersion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductVersion = c.String(),
                        SupportedDesktopTemplate_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Template", t => t.SupportedDesktopTemplate_Id)
                .Index(t => t.SupportedDesktopTemplate_Id);
            
            CreateTable(
                "dbo.Template",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Guid = c.Guid(nullable: false),
                        Content = c.Binary(),
                        ContentHash = c.String(),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Guid, unique: true);
            
            CreateTable(
                "dbo.OnlineProductVersion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductVersion = c.String(),
                        SupportedOnlineTemplate_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Template", t => t.SupportedOnlineTemplate_Id)
                .Index(t => t.SupportedOnlineTemplate_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id", "dbo.Template");
            DropForeignKey("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id", "dbo.Template");
            DropIndex("dbo.OnlineProductVersion", new[] { "SupportedOnlineTemplate_Id" });
            DropIndex("dbo.Template", new[] { "Guid" });
            DropIndex("dbo.DesktopProductVersion", new[] { "SupportedDesktopTemplate_Id" });
            DropTable("dbo.OnlineProductVersion");
            DropTable("dbo.Template");
            DropTable("dbo.DesktopProductVersion");
        }
    }
}
