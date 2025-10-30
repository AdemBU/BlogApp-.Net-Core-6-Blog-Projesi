using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    public partial class SeedCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"), "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1992), null, null, false, null, null, "Asp.Net Core" },
                    { new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"), "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1996), null, null, false, null, null, "Visual Studio" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"), "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(2113), null, null, "images/testimage", "jpg", false, null, null },
                    { new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"), "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(2137), null, null, "images/vstest", "png", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[] { new Guid("53126a25-c896-49a8-9495-e2e070a2f3fa"), new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"), "Bu bir Asp.Net deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1789), null, null, new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"), false, null, null, "Asp.Net Deneme Makalesi", 15 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[] { new Guid("ecbc78a2-9a24-45f0-8e70-6d2031875a9e"), new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"), "Bu bir Visual Studio deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1795), null, null, new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"), false, null, null, "Visual Studio Deneme Makalesi", 15 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("53126a25-c896-49a8-9495-e2e070a2f3fa"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("ecbc78a2-9a24-45f0-8e70-6d2031875a9e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
