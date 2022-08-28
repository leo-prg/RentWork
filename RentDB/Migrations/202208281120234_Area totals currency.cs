namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Areatotalscurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentAreas", "Currency", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentAreas", "Currency");
        }
    }
}
