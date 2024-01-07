namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentObjects", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentObjects", "ImgUrl");
        }
    }
}
