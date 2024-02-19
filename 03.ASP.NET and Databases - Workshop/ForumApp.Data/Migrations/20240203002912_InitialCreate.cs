using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("0bff1d25-54e7-4850-9ac5-9b660e8f7d66"), "Just deployed a sleek new feature on my ASP.NET forum app - check it out and share your thoughts!", "My first post" },
                    { new Guid("8fec07f5-88c2-407c-b72c-3ab771f16f82"), "Facing a challenge in my ASP.NET project - seeking advice from fellow developers. Any insights appreciated!", "My third post" },
                    { new Guid("c21f0f6a-b06e-4054-9f3a-f2cdbf846afc"), "What's your go-to ASP.NET tip? Let's exchange some coding wisdom in our vibrant community!", "My second post" },
                    { new Guid("d5696402-0413-43ac-a342-7930425705df"), "Exciting news! Our ASP.NET forum now supports real-time notifications. Stay connected with the latest discussions effortlessly!", "My fourth post" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
