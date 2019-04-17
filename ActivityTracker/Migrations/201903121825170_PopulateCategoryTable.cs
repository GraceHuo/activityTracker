namespace ActivityTracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id, Name) VALUES (1, 'Sports')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (2, 'Music')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (3, 'Arts')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (4, 'Other')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Categrories WHERE Id IN (1,2,3,4)");
        }
    }
}
