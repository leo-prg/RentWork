namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addrenterunp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Renters", "UNP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Renters", "UNP");
        }
    }
}
