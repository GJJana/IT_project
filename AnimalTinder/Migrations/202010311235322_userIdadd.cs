namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userIdadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "userID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Animals", "userID");
        }
    }
}
