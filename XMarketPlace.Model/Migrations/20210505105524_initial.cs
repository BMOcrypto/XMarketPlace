using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XMarketPlace.Model.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 15, nullable: true),
                    ProductName = table.Column<string>(maxLength: 150, nullable: false),
                    ProductDetail = table.Column<string>(nullable: false),
                    ProductSummary = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: false),
                    UnitsInStock = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    AddToCartCount = table.Column<int>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
