namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameProductVersionToVersionNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DesktopProductVersion", "VersionNumber", c => c.String());
            AddColumn("dbo.OnlineProductVersion", "VersionNumber", c => c.String());
            DropColumn("dbo.DesktopProductVersion", "ProductVersion");
            DropColumn("dbo.OnlineProductVersion", "ProductVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OnlineProductVersion", "ProductVersion", c => c.String());
            AddColumn("dbo.DesktopProductVersion", "ProductVersion", c => c.String());
            DropColumn("dbo.OnlineProductVersion", "VersionNumber");
            DropColumn("dbo.DesktopProductVersion", "VersionNumber");
        }
    }
}
