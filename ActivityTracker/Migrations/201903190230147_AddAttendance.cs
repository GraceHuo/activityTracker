namespace ActivityTracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Attendances",
                    c => new
                    {
                        ActivityId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ActivityId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.ActivityId)
                .Index(t => t.ActivityId)
                .Index(t => t.AttendeeId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropIndex("dbo.Attendances", new[] { "ActivityId" });
            DropTable("dbo.Attendances");
        }
    }
}
