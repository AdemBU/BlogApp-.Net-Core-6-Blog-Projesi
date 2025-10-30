using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    public partial class DalExtensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("53126a25-c896-49a8-9495-e2e070a2f3fa"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("ecbc78a2-9a24-45f0-8e70-6d2031875a9e"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("2063d0e1-a54f-4055-91d5-371cd5fd4ef4"), new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"), "Bu bir Visual Studio deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1371), null, null, new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"), false, null, null, "Visual Studio Deneme Makalesi", 15 },
                    { new Guid("edd430f4-4c17-434c-83ac-c5d5bd38c6ad"), new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"), "Bu bir Asp.Net deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1365), null, null, new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"), false, null, null, "Asp.Net Deneme Makalesi", 15 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1588));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1592));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 18, 25, 24, 790, DateTimeKind.Local).AddTicks(1697));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("2063d0e1-a54f-4055-91d5-371cd5fd4ef4"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("edd430f4-4c17-434c-83ac-c5d5bd38c6ad"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("53126a25-c896-49a8-9495-e2e070a2f3fa"), new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"), "Bu bir Asp.Net deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1789), null, null, new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"), false, null, null, "Asp.Net Deneme Makalesi", 15 },
                    { new Guid("ecbc78a2-9a24-45f0-8e70-6d2031875a9e"), new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"), "Bu bir Visual Studio deneme makalesidir.", "Admin Test", new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1795), null, null, new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"), false, null, null, "Visual Studio Deneme Makalesi", 15 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1992));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(1996));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"),
                column: "CreatedDate",
                value: new DateTime(2025, 10, 30, 14, 49, 19, 94, DateTimeKind.Local).AddTicks(2137));
        }
    }
}
