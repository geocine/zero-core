using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zervo.Data.Models;
using CaseExtensions;
using System.Linq;
using Zervo.Data.Repositories.Contracts;

namespace Zervo.Data.Repositories.Database
{
    public class ZervoContext : DataContext<ZervoContext>
    {

        public ZervoContext(DbContextOptions<ZervoContext> options)
        : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dbSetFinder = this.GetService<IDbSetFinder>();
            var setProperties = dbSetFinder.FindSets(this);

            // Converts column name mapping of C# PascalCase property to snake_case
            // Converts tables name mapping of C# Customers to customers
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }
                // If you reference models on other models you must add it to the DbSet else this will fail
                entity.Relational().TableName = setProperties.Where(d => d.ClrType == entity.ClrType).Select(d => d.Name).First().ToLower();
            }

            /*
             * A one to one relationship
             * http://ef.readthedocs.io/en/latest/modeling/relationships.html#one-to-one
             */
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Customer)
                .WithOne(i => i.Person)
                .HasForeignKey<Customer>(p => p.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Employee)
                .WithOne(i => i.Person)
                .HasForeignKey<Employee>(p => p.PersonId);

        }
    }
}
