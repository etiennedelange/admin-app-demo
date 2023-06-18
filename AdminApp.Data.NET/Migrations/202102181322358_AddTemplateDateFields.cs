namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTemplateDateFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Template", "DateUploaded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Template", "DateLastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DesktopProductVersion", "VersionNumber", c => c.String(maxLength: 10));
            AlterColumn("dbo.OnlineProductVersion", "VersionNumber", c => c.String(maxLength: 10));
            CreateIndex("dbo.DesktopProductVersion", "VersionNumber", unique: true);
            CreateIndex("dbo.OnlineProductVersion", "VersionNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.OnlineProductVersion", new[] { "VersionNumber" });
            DropIndex("dbo.DesktopProductVersion", new[] { "VersionNumber" });
            AlterColumn("dbo.OnlineProductVersion", "VersionNumber", c => c.String());
            AlterColumn("dbo.DesktopProductVersion", "VersionNumber", c => c.String());
            DropColumn("dbo.Template", "DateLastUpdated");
            DropColumn("dbo.Template", "DateUploaded");
        }
    }
}
