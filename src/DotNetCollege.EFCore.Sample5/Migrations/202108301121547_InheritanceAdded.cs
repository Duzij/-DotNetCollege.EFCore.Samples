namespace DotNetCollege.EFCore.Sample5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InheritanceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "Note", c => c.String());
            AddColumn("dbo.Products", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Discriminator");
            DropColumn("dbo.Products", "Note");
            DropColumn("dbo.Products", "Discount");
        }
    }
}
