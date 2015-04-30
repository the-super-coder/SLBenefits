namespace SLBenefits.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 5000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        CreationDate = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                        UpdatetionDate = c.DateTime(defaultValue: DateTime.Now),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Category");
        }
    }
}
