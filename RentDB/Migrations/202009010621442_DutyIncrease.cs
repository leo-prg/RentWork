namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DutyIncrease : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staff", "Duty", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staff", "Duty", c => c.String(maxLength: 30));
        }
    }
}
