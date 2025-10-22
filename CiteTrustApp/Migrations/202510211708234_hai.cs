namespace CiteTrustApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hai : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evidences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Type = c.String(),
                        SourceId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Tags = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Sources", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.SourceId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evidences", "SourceId", "dbo.Sources");
            DropForeignKey("dbo.Evidences", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Evidences", new[] { "CategoryId" });
            DropIndex("dbo.Evidences", new[] { "SourceId" });
            DropTable("dbo.Evidences");
        }
    }
}
