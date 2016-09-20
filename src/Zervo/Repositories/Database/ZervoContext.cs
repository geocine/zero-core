using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Zervo.Models;

namespace Zervo.Repositories.Database
{
    public class ZervoContext : DbContext
    {

        public ZervoContext(DbContextOptions<ZervoContext> options)
        : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

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
                entity.Relational().TableName = setProperties.Where(d => d.ClrType == entity.ClrType).Select(d => d.Name).First().ToLower();
            }
        }
    }
}
