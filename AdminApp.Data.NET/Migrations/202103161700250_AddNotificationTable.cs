namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(),
                        Read = c.Boolean(),
                        DateReceived = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TemplateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Template", t => t.TemplateId)
                .Index(t => t.TemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "TemplateId", "dbo.Template");
            DropIndex("dbo.Notification", new[] { "TemplateId" });
            DropTable("dbo.Notification");
        }
    }
}
