using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    public partial class UserCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("2063d0e1-a54f-4055-91d5-371cd5fd4ef4"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("edd430f4-4c17-434c-83ac-c5d5bd38c6ad"));

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("89cc87eb-fe9c-4391-b49b-2c6edb33a398"), new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"), "Bu bir Visual Studio deneme makalesidir.", "Admin Test", new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9496), null, null, new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"), false, null, null, "Visual Studio Deneme Makalesi", 15 },
                    { new Guid("8a91a29e-7b54-41c5-8193-0d3974ac3215"), new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"), "Bu bir Asp.Net deneme makalesidir.", "Admin Test", new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9490), null, null, new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"), false, null, null, "Asp.Net Deneme Makalesi", 15 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("53eb31b9-071e-4782-961c-d95d9bade071"), "5afbc22e-d07c-4aef-8bf1-df5951c95369", "User", "USER" },
                    { new Guid("99173599-18b0-4271-b2c9-1267003053a7"), "31652448-52ea-4799-bbb0-c0a997e3f67d", "Superadmin", "SUPERADMIN" },
                    { new Guid("c10fa6b4-a853-40fa-ba1a-1a84e25577d3"), "095df00b-e72d-4c06-b18a-c2accdce8f84", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0dd20017-ed70-471f-9701-926a8f764ef2"), 0, "14774fc8-b22f-4071-b850-e95dc19279e9", "superadmin@gmail.com", true, "Adem", "Çok", false, null, "SUPERADMİN@GMAİL.COM", "SUPERADMİN@GMAİL.COM", "AQAAAAEAACcQAAAAEPYVashnhWcgvZSr/paNKdbtQ8qrwoyCfm5EqV1qesWgzlvO115En2GKwiKMihOOAQ==", "+905555555555", true, "7bb8320d-f0a3-4be5-a498-eb6bcfd990d8", false, "superadmin@gmail.com" },
                    { new Guid("5f04df46-aa62-4381-b723-b80ded53a3a0"), 0, "efe85fc9-a26a-4cac-be05-abc3d2ce1e61", "admin@gmail.com", false, "Admin", "User", false, null, "ADMİN@GMAİL.COM", "ADMİN@GMAİL.COM", null, "+905555555556", false, "28eab937-c4ab-41d5-be46-84ef63f61c16", false, "admin@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6af94d9a-243b-449d-a1a6-9831730ab109"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9721));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f72866c-3ad0-44bb-a5b7-21e1ec62b163"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("487f2d55-22de-46dd-ba51-44920c3bec7a"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9839));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("a8c4cebd-5798-4c87-9486-f88c129f267d"),
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 8, 44, 50, 985, DateTimeKind.Local).AddTicks(9850));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("99173599-18b0-4271-b2c9-1267003053a7"), new Guid("0dd20017-ed70-471f-9701-926a8f764ef2") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c10fa6b4-a853-40fa-ba1a-1a84e25577d3"), new Guid("5f04df46-aa62-4381-b723-b80ded53a3a0") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("89cc87eb-fe9c-4391-b49b-2c6edb33a398"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("8a91a29e-7b54-41c5-8193-0d3974ac3215"));

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
    }
}
