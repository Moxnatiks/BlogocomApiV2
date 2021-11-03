using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogocomApiV2.Migrations
{
    public partial class AddMessageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageFiles",
                columns: table => new
                {
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageFiles", x => new { x.MessageId, x.FileId });
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 17, 46, 32, 123, DateTimeKind.Unspecified).AddTicks(6900), new TimeSpan(0, 2, 0, 0, 0)), "Elton.Gorczany36@yahoo.com", "Tiffany", "zCFUq/exYKHCgNFGicnZB3iV32HyvdGrcJeXx7DIU8Q=", new byte[] { 71, 157, 148, 168, 195, 101, 188, 217, 42, 28, 41, 226, 64, 11, 88, 80 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 17, 46, 32, 135, DateTimeKind.Unspecified).AddTicks(4863), new TimeSpan(0, 2, 0, 0, 0)), "Stephanie_Ankunding40@gmail.com", "Delfina", "zCFUq/exYKHCgNFGicnZB3iV32HyvdGrcJeXx7DIU8Q=", new byte[] { 71, 157, 148, 168, 195, 101, 188, 217, 42, 28, 41, 226, 64, 11, 88, 80 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 11, 3, 17, 46, 32, 142, DateTimeKind.Unspecified).AddTicks(8251), new TimeSpan(0, 2, 0, 0, 0)), "Jaden_Reinger55@hotmail.com", "Antwan", "zCFUq/exYKHCgNFGicnZB3iV32HyvdGrcJeXx7DIU8Q=", new byte[] { 71, 157, 148, 168, 195, 101, 188, 217, 42, 28, 41, 226, 64, 11, 88, 80 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageFiles");

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
    }
}
