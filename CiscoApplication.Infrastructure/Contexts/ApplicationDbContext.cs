using CiscoApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CiscoApplication.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        internal DbSet<Item> Items { get; set; }
    }
}
