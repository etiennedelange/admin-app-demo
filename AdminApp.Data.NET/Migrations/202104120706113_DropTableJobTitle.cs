namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTableJobTitle : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.JobTitle");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobTitle",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
