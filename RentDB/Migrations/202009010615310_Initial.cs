namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
           

            CreateTable(
                "dbo.RentAreas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    X = c.Int(nullable: false),
                    Y = c.Int(nullable: false),
                    Width = c.Int(nullable: false),
                    Height = c.Int(nullable: false),
                    Square = c.Single(nullable: false),
                    Price = c.Single(nullable: false),
                    Cost = c.Single(nullable: false),
                    Layer_Id = c.Int(nullable: false),
                    Renter_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentLayers", t => t.Layer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Renters", t => t.Renter_Id)
                .Index(t => t.Layer_Id)
                .Index(t => t.Renter_Id);

            CreateTable(
                "dbo.RentLayers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    LayerFileName = c.String(maxLength: 255),
                    LayerFile = c.Binary(),
                    Width = c.Int(nullable: false),
                    Height = c.Int(nullable: false),
                    Object_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentObjects", t => t.Object_Id, cascadeDelete: true)
                .Index(t => t.Object_Id);

            CreateTable(
                "dbo.RentObjects",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    Address = c.String(maxLength: 150),
                    CNo = c.String(maxLength: 50),
                    Lat = c.String(),
                    Lon = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Renters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RenterName = c.String(maxLength: 200),
                    Contract = c.String(maxLength: 20),
                    StartPartnership = c.DateTime(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    ContactPerson = c.String(),
                    ContactPhone = c.String(),
                    Annotation = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.RentRates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    Rate = c.Single(nullable: false),
                    ForWholeArea = c.Boolean(nullable: false),
                    Note = c.String(maxLength: 100),
                    Area_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentAreas", t => t.Area_Id)
                .Index(t => t.Area_Id);

            CreateTable(
                "dbo.Staff",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    Duty = c.String(maxLength: 30),
                    HomePhone = c.String(maxLength: 50),
                    Email = c.String(maxLength: 30),
                    WorkPhone = c.String(maxLength: 20),
                    InternalPhone = c.String(maxLength: 3),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentRates", "Area_Id", "dbo.RentAreas");
            DropForeignKey("dbo.RentAreas", "Renter_Id", "dbo.Renters");
            DropForeignKey("dbo.RentAreas", "Layer_Id", "dbo.RentLayers");
            DropForeignKey("dbo.RentLayers", "Object_Id", "dbo.RentObjects");
            DropIndex("dbo.RentRates", new[] { "Area_Id" });
            DropIndex("dbo.RentLayers", new[] { "Object_Id" });
            DropIndex("dbo.RentAreas", new[] { "Renter_Id" });
            DropIndex("dbo.RentAreas", new[] { "Layer_Id" });
            DropTable("dbo.Staff");
            DropTable("dbo.RentRates");
            DropTable("dbo.Renters");
            DropTable("dbo.RentObjects");
            DropTable("dbo.RentLayers");
            DropTable("dbo.RentAreas");
        }
    }
}
