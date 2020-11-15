namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtualLikedAnimals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "Description", c => c.String());
            AddColumn("dbo.Animals", "Location", c => c.String());
            AlterColumn("dbo.Animals", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Animals", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "Email", c => c.String());
            AlterColumn("dbo.Animals", "Name", c => c.String());
            DropColumn("dbo.Animals", "Location");
            DropColumn("dbo.Animals", "Description");
        }
    }
}
