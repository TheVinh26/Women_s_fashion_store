namespace ShopDoNu.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
