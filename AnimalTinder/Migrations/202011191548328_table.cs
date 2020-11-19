namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalLikedAnimals",
                c => new
                    {
                        ProfileAnimalId = c.String(nullable: false, maxLength: 128),
                        LikedAnimalId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProfileAnimalId, t.LikedAnimalId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnimalLikedAnimals");
        }
    }
}
