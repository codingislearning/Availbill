namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OTP : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OtpDetails");
            AlterColumn("dbo.OtpDetails", "OtpDetailId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.OtpDetails", "OtpDetailId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OtpDetails");
            AlterColumn("dbo.OtpDetails", "OtpDetailId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.OtpDetails", "OtpDetailId");
        }
    }
}
