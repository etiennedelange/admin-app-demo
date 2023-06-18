namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLinkingTablesForProductVersions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id", "dbo.Template");
            DropForeignKey("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id", "dbo.Template");
            DropIndex("dbo.DesktopProductVersion", new[] { "SupportedDesktopTemplate_Id" });
            DropIndex("dbo.OnlineProductVersion", new[] { "SupportedOnlineTemplate_Id" });
            CreateTable(
                "dbo.TemplateDesktopProductVersion",
                c => new
                    {
                        TemplateId = c.Long(nullable: false),
                        DesktopProductVersionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TemplateId, t.DesktopProductVersionId })
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .ForeignKey("dbo.DesktopProductVersion", t => t.DesktopProductVersionId, cascadeDelete: true)
                .Index(t => t.TemplateId)
                .Index(t => t.DesktopProductVersionId);
            
            CreateTable(
                "dbo.TemplateOnlineProductVersion",
                c => new
                    {
                        TemplateId = c.Long(nullable: false),
                        OnlineProductVersionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TemplateId, t.OnlineProductVersionId })
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .ForeignKey("dbo.OnlineProductVersion", t => t.OnlineProductVersionId, cascadeDelete: true)
                .Index(t => t.TemplateId)
                .Index(t => t.OnlineProductVersionId);
            
            AddColumn("dbo.DesktopProductVersion", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.OnlineProductVersion", "ReleaseDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id");
            DropColumn("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id", c => c.Long());
            AddColumn("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id", c => c.Long());
            DropForeignKey("dbo.TemplateOnlineProductVersion", "OnlineProductVersionId", "dbo.OnlineProductVersion");
            DropForeignKey("dbo.TemplateOnlineProductVersion", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "DesktopProductVersionId", "dbo.DesktopProductVersion");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "TemplateId", "dbo.Template");
            DropIndex("dbo.TemplateOnlineProductVersion", new[] { "OnlineProductVersionId" });
            DropIndex("dbo.TemplateOnlineProductVersion", new[] { "TemplateId" });
            DropIndex("dbo.TemplateDesktopProductVersion", new[] { "DesktopProductVersionId" });
            DropIndex("dbo.TemplateDesktopProductVersion", new[] { "TemplateId" });
            DropColumn("dbo.OnlineProductVersion", "ReleaseDate");
            DropColumn("dbo.DesktopProductVersion", "ReleaseDate");
            DropTable("dbo.TemplateOnlineProductVersion");
            DropTable("dbo.TemplateDesktopProductVersion");
            CreateIndex("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id");
            CreateIndex("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id");
            AddForeignKey("dbo.OnlineProductVersion", "SupportedOnlineTemplate_Id", "dbo.Template", "Id");
            AddForeignKey("dbo.DesktopProductVersion", "SupportedDesktopTemplate_Id", "dbo.Template", "Id");
        }
    }
}
