namespace ActivityTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForActivitiesAndCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Activities", "Teacher_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Activities", new[] { "Category_Id" });
            DropIndex("dbo.Activities", new[] { "Teacher_Id" });
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Activities", "Category_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Activities", "Teacher_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Activities", "Category_Id");
            CreateIndex("dbo.Activities", "Teacher_Id");
            AddForeignKey("dbo.Activities", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "Teacher_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Activities", new[] { "Teacher_Id" });
            DropIndex("dbo.Activities", new[] { "Category_Id" });
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Activities", "Teacher_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Activities", "Category_Id", c => c.Byte());
            AlterColumn("dbo.Activities", "Description", c => c.String());
            CreateIndex("dbo.Activities", "Teacher_Id");
            CreateIndex("dbo.Activities", "Category_Id");
            AddForeignKey("dbo.Activities", "Teacher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Activities", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
