namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchedToDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attorney", "OnlineActivationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ActivationStatusViewResult", "OnlineActivationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.DesktopProductVersion", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Template", "DateUploaded", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Template", "DateLastUpdated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.OnlineProductVersion", "ReleaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Error", "Time", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.LoggedEvent", "TimeStamp", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoggedEvent", "TimeStamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Error", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OnlineProductVersion", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Template", "DateLastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Template", "DateUploaded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DesktopProductVersion", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActivationStatusViewResult", "OnlineActivationDate", c => c.DateTime());
            AlterColumn("dbo.Attorney", "OnlineActivationDate", c => c.DateTime());
        }
    }
}
