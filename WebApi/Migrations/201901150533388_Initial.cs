namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiKeys",
                c => new
                    {
                        ApiKeyId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ApiKeyId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        TransactionId = c.String(maxLength: 128),
                        Detail_Name = c.String(),
                        Detail_Quantity = c.Double(nullable: false),
                        Detail_Rate = c.Double(nullable: false),
                        Detail_Amount = c.Double(nullable: false),
                        Detail_HSN = c.String(),
                        Detail_CGSTPerc = c.Double(nullable: false),
                        Detail_CGSTValue = c.Double(nullable: false),
                        Detail_SGSTPerc = c.Double(nullable: false),
                        Detail_SGSTValue = c.Double(nullable: false),
                        Detail_Discount = c.Double(nullable: false),
                        Detail_Tax = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Transactions", t => t.TransactionId)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        ShopId = c.String(maxLength: 128),
                        TotalBill = c.Double(nullable: false),
                        TransactionDate = c.DateTime(),
                        TransactionNumber = c.String(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Shops", t => t.ShopId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        ShopId = c.String(nullable: false, maxLength: 128),
                        ShopDetails_Name = c.String(),
                        ShopDetails_Location = c.String(),
                        ShopDetails_GSTIN = c.String(),
                        ShopDetails_PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ShopId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        BirthDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Shop_ShopId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.Shop_ShopId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Shop_ShopId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OtpDetails",
                c => new
                    {
                        OtpDetailId = c.String(nullable: false, maxLength: 128),
                        GeneratedTimeStamp = c.DateTime(nullable: false),
                        ExpiryTimeStamp = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        Otp = c.String(),
                        Message = c.String(),
                        Type = c.String(),
                        RequestCount = c.Int(nullable: false),
                        OtpVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OtpDetailId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Shop_ShopId", "dbo.Shops");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.Items", "TransactionId", "dbo.Transactions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Shop_ShopId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Transactions", new[] { "ShopId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "TransactionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OtpDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Shops");
            DropTable("dbo.Transactions");
            DropTable("dbo.Items");
            DropTable("dbo.ApiKeys");
        }
    }
}
