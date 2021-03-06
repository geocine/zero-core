﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Zervo.Data.Repositories.Database;

namespace Zervo.Data.Migrations
{
    [DbContext(typeof(ZervoContext))]
    [Migration("20161022080137_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Zervo.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("PersonId")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Zervo.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<float>("HourlyWage")
                        .HasColumnName("hourly_wage");

                    b.Property<int>("PersonId")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Zervo.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Zervo.Data.Models.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Type")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("tokens");
                });

            modelBuilder.Entity("Zervo.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Zervo.Data.Models.Customer", b =>
                {
                    b.HasOne("Zervo.Data.Models.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("Zervo.Data.Models.Customer", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zervo.Data.Models.Employee", b =>
                {
                    b.HasOne("Zervo.Data.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("Zervo.Data.Models.Employee", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
