namespace ActivityTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceledToActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "IsCanceled");
        }
    }
}
