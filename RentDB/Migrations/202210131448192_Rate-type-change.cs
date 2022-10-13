namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratetypechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentRates", "Rate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentRates", "Rate", c => c.Single(nullable: false));
        }
    }
}
