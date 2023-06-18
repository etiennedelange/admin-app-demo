namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingTableChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Setting", "EnabledGlobally", c => c.Boolean());
            DropColumn("dbo.Setting", "Enabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Setting", "Enabled", c => c.Boolean());
            DropColumn("dbo.Setting", "EnabledGlobally");
        }
    }
}
