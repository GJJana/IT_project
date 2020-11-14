namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "Breed", c => c.String());
            AddColumn("dbo.Animals", "Gender", c => c.String());
            DropColumn("dbo.Animals", "Sort");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Sort", c => c.String());
            DropColumn("dbo.Animals", "Gender");
            DropColumn("dbo.Animals", "Breed");
        }
    }
}
