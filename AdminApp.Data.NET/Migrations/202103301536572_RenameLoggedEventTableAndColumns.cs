namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameLoggedEventTableAndColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        User = c.String(),
                        Path = c.String(),
                        ActionDescription = c.String(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.LoggedEvent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoggedEvent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UpdatedBy = c.String(),
                        UpdatedItem = c.String(),
                        Action = c.String(),
                        ActionDescription = c.String(),
                        TimeStamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.AuditLog");
        }
    }
}
