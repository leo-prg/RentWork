namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenterAccCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Renters", "AccCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Renters", "AccCode");
        }
    }
}
