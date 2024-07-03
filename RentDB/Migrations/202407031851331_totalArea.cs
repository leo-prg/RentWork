namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentObjects", "TotalArea", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentObjects", "TotalArea");
        }
    }
}
