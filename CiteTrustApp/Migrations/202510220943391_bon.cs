namespace CiteTrustApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bon : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sources", "Publisher", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sources", "Publisher", c => c.Int(nullable: false));
        }
    }
}
