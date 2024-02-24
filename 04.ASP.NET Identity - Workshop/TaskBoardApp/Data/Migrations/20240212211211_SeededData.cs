using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "507a2485-c771-44d9-afcf-b44751d633f7", 0, "944c146d-579e-4d7f-b1fd-708067d14376", null, false, false, null, null, "STELIILIEV920@GMAIL.COM", "AQAAAAEAACcQAAAAEN1HfrSfRZuWJZET2PW+lI4pDZBNvVL+EuT3JiVAoDG/dwm1LXtn+5HCfgHhzBCbGQ==", null, false, "ed1b4851-fd12-4488-a3f1-32e32951cb17", false, "steliiliev920@gmail.com" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 27, 23, 12, 10, 979, DateTimeKind.Local).AddTicks(9365), "Implement better styling for all public pages", "507a2485-c771-44d9-afcf-b44751d633f7", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 12, 23, 12, 10, 979, DateTimeKind.Local).AddTicks(9464), "Create Android client app for the TaskBoard RESTful API", "507a2485-c771-44d9-afcf-b44751d633f7", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 12, 23, 12, 10, 979, DateTimeKind.Local).AddTicks(9473), "Create Windows Forms appclient for the TaskBoard RESTful API", "507a2485-c771-44d9-afcf-b44751d633f7", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 12, 23, 12, 10, 979, DateTimeKind.Local).AddTicks(9476), "Implement [Create Task] page for adding new tasks", "507a2485-c771-44d9-afcf-b44751d633f7", "Create Tasks" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507a2485-c771-44d9-afcf-b44751d633f7");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
