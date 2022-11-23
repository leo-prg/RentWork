namespace RentData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmploymentDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staff", "EmpDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staff", "EmpDate");
        }
    }
}
