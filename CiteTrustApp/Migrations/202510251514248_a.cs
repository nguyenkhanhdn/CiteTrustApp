namespace CiteTrustApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsOnboarded", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "InterestCategoryIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "InterestCategoryIds");
            DropColumn("dbo.AspNetUsers", "IsOnboarded");
        }
    }
}
