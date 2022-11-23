namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ratecurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentRates", "Currency", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentRates", "Currency");
        }
    }
}
