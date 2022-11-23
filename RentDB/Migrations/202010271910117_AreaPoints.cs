namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AreaPoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AreaPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PointNo = c.Int(nullable: false),
                        PointX = c.Int(nullable: false),
                        PointY = c.Int(nullable: false),
                        Area_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentAreas", t => t.Area_Id)
                .Index(t => t.Area_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AreaPoints", "Area_Id", "dbo.RentAreas");
            DropIndex("dbo.AreaPoints", new[] { "Area_Id" });
            DropTable("dbo.AreaPoints");
        }
    }
}
