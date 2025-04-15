using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItemTag>(mt => mt.HasKey(mt => new { mt.MenuItemId, mt.TagId }));

            modelBuilder.Entity<MenuItemTag>()
                .HasOne(mt => mt.MenuItem)
                .WithMany(m => m.MenuItemTags)
                .HasForeignKey(mt => mt.MenuItemId);

            modelBuilder.Entity<MenuItemTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.MenuItemTags)
                .HasForeignKey(mt => mt.TagId);
        }
    }
    
}