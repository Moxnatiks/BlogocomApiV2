﻿// <auto-generated />
using System;
using BlogocomApiV2.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogocomApiV2.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20211022114111_AddMessageChatUserChats")]
    partial class AddMessageChatUserChats
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BlogocomApiV2.Models.Chat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsPrivate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("BlogocomApiV2.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("BlogocomApiV2.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsAccess")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<byte[]>("StoredSalt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedDate = new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 483, DateTimeKind.Unspecified).AddTicks(9244), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "Ethelyn48@yahoo.com",
                            FirstName = "Bell",
                            IsAccess = true,
                            Password = "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=",
                            Phone = "+380994444333",
                            StoredSalt = new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 }
                        },
                        new
                        {
                            Id = 2L,
                            CreatedDate = new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 505, DateTimeKind.Unspecified).AddTicks(6484), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "Justyn.Considine@hotmail.com",
                            FirstName = "Kavon",
                            IsAccess = true,
                            Password = "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=",
                            Phone = "+380994444222",
                            StoredSalt = new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 }
                        },
                        new
                        {
                            Id = 3L,
                            CreatedDate = new DateTimeOffset(new DateTime(2021, 10, 22, 14, 41, 10, 522, DateTimeKind.Unspecified).AddTicks(3170), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "Chelsie_Moen@hotmail.com",
                            FirstName = "Brooks",
                            IsAccess = true,
                            Password = "wcWaGYoBC6/gGwu9F/dXNZ4UDlbTEQjwj6sd5on6pVk=",
                            Phone = "+380994444111",
                            StoredSalt = new byte[] { 74, 154, 117, 164, 154, 168, 10, 101, 252, 3, 116, 224, 219, 182, 135, 138 }
                        });
                });

            modelBuilder.Entity("BlogocomApiV2.Models.UserChat", b =>
                {
                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("ChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<long>("ChatsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("ChatsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("BlogocomApiV2.Models.Message", b =>
                {
                    b.HasOne("BlogocomApiV2.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogocomApiV2.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogocomApiV2.Models.UserChat", b =>
                {
                    b.HasOne("BlogocomApiV2.Models.Chat", "Chat")
                        .WithMany("UserChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogocomApiV2.Models.User", "User")
                        .WithMany("UserChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.HasOne("BlogocomApiV2.Models.Chat", null)
                        .WithMany()
                        .HasForeignKey("ChatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogocomApiV2.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogocomApiV2.Models.Chat", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChats");
                });

            modelBuilder.Entity("BlogocomApiV2.Models.User", b =>
                {
                    b.Navigation("UserChats");
                });
#pragma warning restore 612, 618
        }
    }
}
