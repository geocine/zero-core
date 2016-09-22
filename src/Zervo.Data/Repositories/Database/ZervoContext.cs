using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Zervo.Data.Models;
using CaseExtensions;
using System.Linq;

namespace Zervo.Data.Repositories.Database
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
