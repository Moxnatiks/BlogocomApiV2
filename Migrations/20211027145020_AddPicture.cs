using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogocomApiV2.Migrations
{
    public partial class AddPicture : Migration
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
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OriginalName = table.Column<string>(type: "text", nullable: true),
                    UniqueName = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    WebPath = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 17, 50, 19, 171, DateTimeKind.Unspecified).AddTicks(9325), new TimeSpan(0, 3, 0, 0, 0)), "Josianne_Davis45@yahoo.com", "Taylor", "qSbCg9Osolw54NC6gZb5KXHtV9YNuYCa1NCxpMQURmM=", new byte[] { 223, 3, 251, 135, 71, 109, 146, 5, 187, 105, 213, 9, 243, 45, 244, 43 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 17, 50, 19, 186, DateTimeKind.Unspecified).AddTicks(5941), new TimeSpan(0, 3, 0, 0, 0)), "Blair_Zieme@yahoo.com", "Mercedes", "qSbCg9Osolw54NC6gZb5KXHtV9YNuYCa1NCxpMQURmM=", new byte[] { 223, 3, 251, 135, 71, 109, 146, 5, 187, 105, 213, 9, 243, 45, 244, 43 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 17, 50, 19, 194, DateTimeKind.Unspecified).AddTicks(4050), new TimeSpan(0, 3, 0, 0, 0)), "Robb64@gmail.com", "D'angelo", "qSbCg9Osolw54NC6gZb5KXHtV9YNuYCa1NCxpMQURmM=", new byte[] { 223, 3, 251, 135, 71, 109, 146, 5, 187, 105, 213, 9, 243, 45, 244, 43 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

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
