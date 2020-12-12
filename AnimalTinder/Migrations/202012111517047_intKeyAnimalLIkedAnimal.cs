namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intKeyAnimalLIkedAnimal : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AnimalLikedAnimals");
            AlterColumn("dbo.AnimalLikedAnimals", "ProfileAnimalId", c => c.Int(nullable: false));
            AlterColumn("dbo.AnimalLikedAnimals", "LikedAnimalId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AnimalLikedAnimals", new[] { "ProfileAnimalId", "LikedAnimalId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AnimalLikedAnimals");
            AlterColumn("dbo.AnimalLikedAnimals", "LikedAnimalId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AnimalLikedAnimals", "ProfileAnimalId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AnimalLikedAnimals", new[] { "ProfileAnimalId", "LikedAnimalId" });
        }
    }
}
