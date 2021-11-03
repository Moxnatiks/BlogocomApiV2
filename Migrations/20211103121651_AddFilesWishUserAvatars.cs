using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogocomApiV2.Migrations
{
    public partial class AddFilesWishUserAvatars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPrivate",
                table: "Chats",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OriginalName = table.Column<string>(type: "text", nullable: true),
                    UniqueName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    WebPath = table.Column<string>(type: "text", nullable: true),
                    PreviewVideoPicWebPart = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAvatars",
                columns: table => new
                {
                    PictureId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAvatars", x => new { x.PictureId, x.UserId });
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 14, 16, 50, 685, DateTimeKind.Unspecified).AddTicks(4734), new TimeSpan(0, 2, 0, 0, 0)), "Chelsea49@hotmail.com", "Brenna", "nfay9OK8biueEFIJyZNJQhbekzQRpjC4za/GVj+9cFo=", new byte[] { 174, 67, 8, 235, 43, 3, 117, 189, 133, 175, 131, 127, 44, 128, 125, 156 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 14, 16, 50, 703, DateTimeKind.Unspecified).AddTicks(9551), new TimeSpan(0, 2, 0, 0, 0)), "Katherine_Howell32@yahoo.com", "Eveline", "nfay9OK8biueEFIJyZNJQhbekzQRpjC4za/GVj+9cFo=", new byte[] { 174, 67, 8, 235, 43, 3, 117, 189, 133, 175, 131, 127, 44, 128, 125, 156 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 14, 16, 50, 717, DateTimeKind.Unspecified).AddTicks(4667), new TimeSpan(0, 2, 0, 0, 0)), "Reynold22@gmail.com", "Jonathon", "nfay9OK8biueEFIJyZNJQhbekzQRpjC4za/GVj+9cFo=", new byte[] { 174, 67, 8, 235, 43, 3, 117, 189, 133, 175, 131, 127, 44, 128, 125, 156 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "UserAvatars");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPrivate",
                table: "Chats",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 483, DateTimeKind.Unspecified).AddTicks(9244), new TimeSpan(0, 3, 0, 0, 0)), "Ethelyn48@yahoo.com", "Bell", "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=", new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 505, DateTimeKind.Unspecified).AddTicks(6484), new TimeSpan(0, 3, 0, 0, 0)), "Justyn.Considine@hotmail.com", "Kavon", "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=", new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 522, DateTimeKind.Unspecified).AddTicks(3170), new TimeSpan(0, 3, 0, 0, 0)), "Chelsie_Moen@hotmail.com", "Brooks", "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=", new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 } });
        }
    }
}
