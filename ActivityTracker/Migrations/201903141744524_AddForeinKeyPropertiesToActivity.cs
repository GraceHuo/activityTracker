namespace ActivityTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeinKeyPropertiesToActivity : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Activities", name: "Category_Id", newName: "CategoryId");
            RenameColumn(table: "dbo.Activities", name: "Teacher_Id", newName: "TeacherId");
            RenameIndex(table: "dbo.Activities", name: "IX_Teacher_Id", newName: "IX_TeacherId");
            RenameIndex(table: "dbo.Activities", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Activities", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameIndex(table: "dbo.Activities", name: "IX_TeacherId", newName: "IX_Teacher_Id");
            RenameColumn(table: "dbo.Activities", name: "TeacherId", newName: "Teacher_Id");
            RenameColumn(table: "dbo.Activities", name: "CategoryId", newName: "Category_Id");
        }
    }
}
