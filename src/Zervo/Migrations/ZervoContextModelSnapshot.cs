using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Zervo.Repositories.Database;

namespace Zervo.Migrations
{
    [DbContext(typeof(ZervoContext))]
    partial class ZervoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");
                
            modelBuilder.Entity("Zervo.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("customers");
                });
        }
    }
}
