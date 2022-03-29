namespace ProtonList.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnChangesId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.User", name: "LastName", newName: "Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.User", name: "Id", newName: "LastName");
        }
    }
}
