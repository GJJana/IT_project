namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Animals", "ImgURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "ImgURL", c => c.String());
            AlterColumn("dbo.Animals", "Type", c => c.String());
        }
    }
}
