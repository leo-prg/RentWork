namespace RentDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RenterFiles", name: "Renter_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.RenterFiles", name: "IX_Renter_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RenterFiles", name: "IX_Owner_Id", newName: "IX_Renter_Id");
            RenameColumn(table: "dbo.RenterFiles", name: "Owner_Id", newName: "Renter_Id");
        }
    }
}
