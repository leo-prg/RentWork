namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileStorage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RenterFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Document = c.Binary(),
                        Description = c.String(),
                        FileName = c.String(),
                        FileSize = c.Decimal(nullable: false, precision: 18, scale: 0),
                        Modified = c.DateTime(nullable: false),
                        Renter_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Renters", t => t.Renter_Id, cascadeDelete: true)
                .Index(t => t.Renter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RenterFiles", "Renter_Id", "dbo.Renters");
            DropIndex("dbo.RenterFiles", new[] { "Renter_Id" });
            DropTable("dbo.RenterFiles");
        }
    }
}
