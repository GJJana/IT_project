namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteListLocal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Animals", "Animal_ID", "dbo.Animals");
            DropIndex("dbo.Animals", new[] { "Animal_ID" });
            DropColumn("dbo.Animals", "Animal_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Animal_ID", c => c.Int());
            CreateIndex("dbo.Animals", "Animal_ID");
            AddForeignKey("dbo.Animals", "Animal_ID", "dbo.Animals", "ID");
        }
    }
}
