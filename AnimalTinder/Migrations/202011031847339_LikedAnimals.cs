namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikedAnimals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "Type", c => c.String());
            AddColumn("dbo.Animals", "Sort", c => c.String());
            AddColumn("dbo.Animals", "Animal_ID", c => c.Int());
            CreateIndex("dbo.Animals", "Animal_ID");
            AddForeignKey("dbo.Animals", "Animal_ID", "dbo.Animals", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "Animal_ID", "dbo.Animals");
            DropIndex("dbo.Animals", new[] { "Animal_ID" });
            DropColumn("dbo.Animals", "Animal_ID");
            DropColumn("dbo.Animals", "Sort");
            DropColumn("dbo.Animals", "Type");
        }
    }
}
