namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportsCleanup : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ActivationStatusViewResult");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActivationStatusViewResult",
                c => new
                    {
                        Kref = c.Guid(nullable: false),
                        Name = c.String(),
                        IsHostedFirm = c.Boolean(nullable: false),
                        DebtorCode = c.String(),
                        TransfersEnabled = c.Int(nullable: false),
                        ABSABankEnabled = c.Int(nullable: false),
                        StandardBankEnabled = c.Int(nullable: false),
                        FNBHLEnabled = c.Int(nullable: false),
                        FNBConsentsHLEnabled = c.Int(nullable: false),
                        FNBHFEnabled = c.Int(nullable: false),
                        FNBPCEnabled = c.Int(nullable: false),
                        FNBPWEnabled = c.Int(nullable: false),
                        NedbankEnabled = c.Int(nullable: false),
                        NedbankConsentsEnabled = c.Int(nullable: false),
                        RMBPWEnabled = c.Int(nullable: false),
                        OnlineActivationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Kref);
            
        }
    }
}
