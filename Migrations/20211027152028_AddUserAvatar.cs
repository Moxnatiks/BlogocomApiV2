using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogocomApiV2.Migrations
{
    public partial class AddUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 18, 20, 27, 925, DateTimeKind.Unspecified).AddTicks(8950), new TimeSpan(0, 3, 0, 0, 0)), "Lou56@gmail.com", "Helene", "lH31c03BWkO4E1bdM7iXBJ/dX6X7vqpEa5jVRrAmBQU=", new byte[] { 66, 27, 48, 135, 126, 197, 147, 180, 22, 135, 245, 186, 236, 21, 161, 213 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 18, 20, 27, 937, DateTimeKind.Unspecified).AddTicks(1548), new TimeSpan(0, 3, 0, 0, 0)), "Laverna_Kerluke99@hotmail.com", "Roger", "lH31c03BWkO4E1bdM7iXBJ/dX6X7vqpEa5jVRrAmBQU=", new byte[] { 66, 27, 48, 135, 126, 197, 147, 180, 22, 135, 245, 186, 236, 21, 161, 213 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 27, 18, 20, 27, 944, DateTimeKind.Unspecified).AddTicks(5491), new TimeSpan(0, 3, 0, 0, 0)), "Kiarra.Morissette71@hotmail.com", "Kip", "lH31c03BWkO4E1bdM7iXBJ/dX6X7vqpEa5jVRrAmBQU=", new byte[] { 66, 27, 48, 135, 126, 197, 147, 180, 22, 135, 245, 186, 236, 21, 161, 213 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAvatars");

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
    }
}
