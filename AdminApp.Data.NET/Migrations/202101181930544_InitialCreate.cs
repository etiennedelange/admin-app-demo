namespace AdminApp.Data.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attorney",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Kref = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        LUN = c.String(),
                        ALT_LUN = c.String(),
                        DebtorCode = c.String(),
                        Branch = c.String(),
                        OnlineActivationChecked = c.Boolean(nullable: false),
                        OnlineActivationDate = c.DateTime(),
                        IsHostedFirm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Kref, unique: true);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Description = c.String(),
                        Enabled = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Guid, unique: true);
            
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
                        OnlineActivationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Kref);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Callstack = c.String(),
                        Time = c.DateTime(nullable: false),
                        Message = c.String(),
                        User = c.String(),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobTitle",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoggedEvent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UpdatedBy = c.String(),
                        UpdatedItem = c.String(),
                        Action = c.String(),
                        ActionDescription = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttorneySetting",
                c => new
                    {
                        AttorneyId = c.Long(nullable: false),
                        SettingId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AttorneyId, t.SettingId })
                .ForeignKey("dbo.Attorney", t => t.AttorneyId, cascadeDelete: true)
                .ForeignKey("dbo.Setting", t => t.SettingId, cascadeDelete: true)
                .Index(t => t.AttorneyId)
                .Index(t => t.SettingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttorneySetting", "SettingId", "dbo.Setting");
            DropForeignKey("dbo.AttorneySetting", "AttorneyId", "dbo.Attorney");
            DropIndex("dbo.AttorneySetting", new[] { "SettingId" });
            DropIndex("dbo.AttorneySetting", new[] { "AttorneyId" });
            DropIndex("dbo.Setting", new[] { "Guid" });
            DropIndex("dbo.Attorney", new[] { "Kref" });
            DropTable("dbo.AttorneySetting");
            DropTable("dbo.LoggedEvent");
            DropTable("dbo.JobTitle");
            DropTable("dbo.Error");
            DropTable("dbo.ActivationStatusViewResult");
            DropTable("dbo.Setting");
            DropTable("dbo.Attorney");
        }
    }
}
