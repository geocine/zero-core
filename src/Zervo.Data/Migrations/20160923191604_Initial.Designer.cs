using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Zervo.Data.Repositories.Database;

namespace Zervo.Data.Migrations
{
    [DbContext(typeof(ZervoContext))]
    [Migration("20160923191604_Initial")]
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

            modelBuilder.Entity("Zervo.Data.Models.Person", b =>
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

                    b.HasKey("Id");

                    b.ToTable("people");
                });

            modelBuilder.Entity("Zervo.Data.Models.Customer", b =>
                {
                    b.HasOne("Zervo.Data.Models.Person", "Person")
                        .WithOne("Customer")
                        .HasForeignKey("Zervo.Data.Models.Customer", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
