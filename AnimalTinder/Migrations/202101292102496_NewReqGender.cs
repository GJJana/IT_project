namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReqGender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "Gender", c => c.String());
        }
    }
}
