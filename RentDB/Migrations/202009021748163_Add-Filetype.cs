namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFiletype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RenterFiles", "FileType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RenterFiles", "FileType");
        }
    }
}
