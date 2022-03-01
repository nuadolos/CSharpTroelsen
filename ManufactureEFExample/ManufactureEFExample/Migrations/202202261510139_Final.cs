namespace ManufactureEFExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.User", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            CreateIndex("dbo.User", new[] { "Login", "Password" }, unique: true, name: "IDX_User_Authorization");
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Timestamp");
            DropColumn("dbo.Role", "Timestamp");
            DropIndex("dbo.User", "IDX_User_Authorization");
        }
    }
}
