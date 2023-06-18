namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCascadeDeleteConvention : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney");
            DropForeignKey("dbo.AttorneySetting", "SettingId", "dbo.Setting");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "DesktopProductVersionId", "dbo.DesktopProductVersion");
            DropForeignKey("dbo.TemplateOnlineProductVersion", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.TemplateOnlineProductVersion", "OnlineProductVersionId", "dbo.OnlineProductVersion");
            AddForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney", "Id");
            AddForeignKey("dbo.AttorneySetting", "SettingId", "dbo.Setting", "Id");
            AddForeignKey("dbo.TemplateDesktopProductVersion", "TemplateId", "dbo.Template", "Id");
            AddForeignKey("dbo.TemplateDesktopProductVersion", "DesktopProductVersionId", "dbo.DesktopProductVersion", "Id");
            AddForeignKey("dbo.TemplateOnlineProductVersion", "TemplateId", "dbo.Template", "Id");
            AddForeignKey("dbo.TemplateOnlineProductVersion", "OnlineProductVersionId", "dbo.OnlineProductVersion", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemplateOnlineProductVersion", "OnlineProductVersionId", "dbo.OnlineProductVersion");
            DropForeignKey("dbo.TemplateOnlineProductVersion", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "DesktopProductVersionId", "dbo.DesktopProductVersion");
            DropForeignKey("dbo.TemplateDesktopProductVersion", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.AttorneySetting", "SettingId", "dbo.Setting");
            DropForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney");
            AddForeignKey("dbo.TemplateOnlineProductVersion", "OnlineProductVersionId", "dbo.OnlineProductVersion", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TemplateOnlineProductVersion", "TemplateId", "dbo.Template", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TemplateDesktopProductVersion", "DesktopProductVersionId", "dbo.DesktopProductVersion", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TemplateDesktopProductVersion", "TemplateId", "dbo.Template", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AttorneySetting", "SettingId", "dbo.Setting", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney", "Id", cascadeDelete: true);
        }
    }
}
