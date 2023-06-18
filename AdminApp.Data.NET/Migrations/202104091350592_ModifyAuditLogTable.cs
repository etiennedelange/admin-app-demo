namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAuditLogTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLog", "Data", c => c.String());
            DropColumn("dbo.AuditLog", "Path");
            DropColumn("dbo.AuditLog", "ActionDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditLog", "ActionDescription", c => c.String());
            AddColumn("dbo.AuditLog", "Path", c => c.String());
            DropColumn("dbo.AuditLog", "Data");
        }
    }
}
