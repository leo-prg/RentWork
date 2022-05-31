namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        DateOper = c.DateTime(nullable: false),
                        Page = c.String(),
                        Oper = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RentLogs");
        }
    }
}
