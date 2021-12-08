using BestGiftsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.DAL
{
    public class BestGiftsContext : DbContext
    {
        public BestGiftsContext(DbContextOptions<BestGiftsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GiftIdeaCategory>().HasKey(gic => new { gic.GiftIdeaId, gic.CategoryId });
        }

        public DbSet<GiftIdea> GiftIdeas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GiftIdeaCategory> GiftIdeaCategories { get; set; }
    }
}
