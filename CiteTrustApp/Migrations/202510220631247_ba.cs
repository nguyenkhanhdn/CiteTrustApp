namespace CiteTrustApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ba : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evidences", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Evidences", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Evidences", "Tags", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evidences", "Tags", c => c.String());
            AlterColumn("dbo.Evidences", "Type", c => c.String());
            AlterColumn("dbo.Evidences", "Content", c => c.String());
        }
    }
}
