using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogocomApiV2.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IsAccess = table.Column<bool>(type: "boolean", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    StoredSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsAccess", "Password", "Phone", "StoredSalt" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 827, DateTimeKind.Unspecified).AddTicks(754), new TimeSpan(0, 3, 0, 0, 0)), "Madge54@yahoo.com", "Preston", true, "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", "+380994444333", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } },
                    { 2L, new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 837, DateTimeKind.Unspecified).AddTicks(3884), new TimeSpan(0, 3, 0, 0, 0)), "Alysson_Bayer29@yahoo.com", "Antonetta", true, "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", "+380994444222", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } },
                    { 3L, new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 845, DateTimeKind.Unspecified).AddTicks(5301), new TimeSpan(0, 3, 0, 0, 0)), "Carlee_Kemmer@hotmail.com", "Lia", true, "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", "+380994444111", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
