﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Otus.Project.Orm.Configuration;

namespace Otus.Project.Orm.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20211109141424_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Otus.Project.Domain.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CellPhone")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsEmailNotificationEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae1"),
                            CellPhone = "1111-111-111",
                            CreatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810),
                            Email = "john.dow.1@example.com",
                            FirstName = "John",
                            IsEmailNotificationEnabled = true,
                            LastName = "Dow",
                            MiddleName = "1st",
                            Password = "some_pass_1",
                            UpdatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810)
                        },
                        new
                        {
                            Id = new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae2"),
                            CellPhone = "1111-222-222",
                            CreatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810),
                            Email = "john.dow.2@example.com",
                            FirstName = "John",
                            IsEmailNotificationEnabled = true,
                            LastName = "Dow",
                            MiddleName = "2nd",
                            Password = "some_pass_2",
                            UpdatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810)
                        },
                        new
                        {
                            Id = new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae3"),
                            CellPhone = "1111-333-333",
                            CreatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810),
                            Email = "john.dow.3@example.com",
                            FirstName = "John",
                            IsEmailNotificationEnabled = true,
                            LastName = "Dow",
                            MiddleName = "3rd",
                            Password = "some_pass_3",
                            UpdatedDate = new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
