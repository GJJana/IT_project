namespace AnimalTinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withoutIMGReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "ImgURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "ImgURL", c => c.String(nullable: false));
        }
    }
}
