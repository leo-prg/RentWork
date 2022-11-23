namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeDoB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "DateOfBirth", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staff", "DateOfBirth");
        }
    }
}
