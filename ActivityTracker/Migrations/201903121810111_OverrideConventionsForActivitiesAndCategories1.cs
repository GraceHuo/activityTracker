namespace ActivityTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForActivitiesAndCategories1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Location", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "Location", c => c.String());
        }
    }
}
