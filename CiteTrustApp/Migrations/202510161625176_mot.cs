namespace CiteTrustApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Publisher = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        Credi_score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sources");
            DropTable("dbo.Categories");
        }
    }
}
