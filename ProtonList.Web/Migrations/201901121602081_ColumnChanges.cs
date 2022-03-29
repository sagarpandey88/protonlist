namespace ProtonList.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnChanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.User", name: "Id", newName: "LastName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.User", name: "LastName", newName: "Id");
        }
    }
}
