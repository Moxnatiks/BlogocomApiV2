using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogocomApiV2.Migrations
{
    public partial class AddMessageChatUserChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => new { x.ChatsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ChatUser_Chats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => new { x.ChatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ChatUser_UsersId",
                table: "ChatUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_UserId",
                table: "UserChats",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 827, DateTimeKind.Unspecified).AddTicks(754), new TimeSpan(0, 3, 0, 0, 0)), "Madge54@yahoo.com", "Preston", "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 837, DateTimeKind.Unspecified).AddTicks(3884), new TimeSpan(0, 3, 0, 0, 0)), "Alysson_Bayer29@yahoo.com", "Antonetta", "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "Email", "FirstName", "Password", "StoredSalt" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 10, 21, 15, 21, 50, 845, DateTimeKind.Unspecified).AddTicks(5301), new TimeSpan(0, 3, 0, 0, 0)), "Carlee_Kemmer@hotmail.com", "Lia", "AcvlO60SntLTg24Kfon2Dypz0Req/tWyHST8hZ0Yljk=", new byte[] { 80, 19, 190, 77, 38, 18, 236, 73, 57, 222, 141, 155, 65, 197, 29, 43 } });
        }
    }
}
