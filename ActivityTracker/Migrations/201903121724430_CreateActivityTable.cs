namespace ActivityTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateActivityTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        Category_Id = c.Byte(),
                        Teacher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Activities", new[] { "Teacher_Id" });
            DropIndex("dbo.Activities", new[] { "Category_Id" });
            DropTable("dbo.Categories");
            DropTable("dbo.Activities");
        }
    }
}
