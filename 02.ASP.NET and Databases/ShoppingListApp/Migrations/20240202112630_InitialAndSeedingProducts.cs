using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingListApp.Migrations
{
    public partial class InitialAndSeedingProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Product Identifier"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Product Name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                },
                comment: "Shopping List Product");

            migrationBuilder.CreateTable(
                name: "ProductNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ProductNote Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "ProductNote Content"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Product Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductNotes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Shopping List ProductNote");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cheese" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Milk" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductNotes_ProductId",
                table: "ProductNotes",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductNotes");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
