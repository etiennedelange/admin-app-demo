namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDeleteForFirms : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney");
            AddForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney");
            AddForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney", "Id");
        }
    }
}
