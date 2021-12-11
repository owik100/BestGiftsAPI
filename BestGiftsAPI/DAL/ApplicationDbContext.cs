using BestGiftsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.DAL
{
    public class ApplicationDbContext : DbContext
    {
        ModelBuilder _modelBuilder;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            _modelBuilder = modelBuilder;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GiftIdeaCategory>().HasKey(gic => new { gic.GiftIdeaId, gic.CategoryId });

            SeedDataBase();
        }

        public DbSet<GiftIdea> GiftIdeas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GiftIdeaCategory> GiftIdeaCategories { get; set; }

        private void SeedDataBase()
        {
            List<Comment> comments1 = new List<Comment>
            {
                new Comment(){CommentId = 1, GiftIdeaId = 1, CommentContent = "Super pomysł!", CommentAuthor ="Zbyszek"},
                new Comment(){CommentId = 2, GiftIdeaId = 2, CommentContent = "Nudy", CommentAuthor ="Karolina"},
                new Comment(){CommentId = 3, GiftIdeaId = 2, CommentContent = "Ale to drogie", CommentAuthor ="Krzychux1200"},
            };

            List<Category> categories1 = new List<Category>
            {
                new Category(){CategoryId = 1, Name = "Dla niego" },
                new Category(){CategoryId = 2, Name = "Dla niej" },
                new Category(){CategoryId = 3, Name = "Na urodziny" }
            };

            List<GiftIdeaCategory> giftIdeaCategories1 = new List<GiftIdeaCategory>
            {
                new GiftIdeaCategory(){GiftIdeaId = 1, CategoryId = 3},
                new GiftIdeaCategory(){GiftIdeaId = 1, CategoryId = 1},
                new GiftIdeaCategory(){GiftIdeaId = 2, CategoryId = 1},
                new GiftIdeaCategory(){GiftIdeaId = 2, CategoryId = 2},
                new GiftIdeaCategory(){GiftIdeaId = 2, CategoryId = 3},
                new GiftIdeaCategory(){GiftIdeaId = 3, CategoryId = 3},
            };

            List<GiftIdea> giftIdeas = new List<GiftIdea>
            {
                   new GiftIdea(){Name = "Kolacja we dwoje", Author ="Jan", Description = "Można kupić np. na twoje_marzenia.com", GiftIdeaId = 1, LikesCounter = 5, },
                   new GiftIdea(){Name = "Kubek termiczny", Author ="Maciek", Description = "Zmienia kolor w zależności od temperatuy napoju", GiftIdeaId = 2, LikesCounter = 2, },
                   new GiftIdea(){Name = "Skarpety", Author ="Paulina", Description = "Własnoręcznie wydziergane skarpety. Czy może być coś lepszego? :)", GiftIdeaId = 3, LikesCounter = -3, },
            };

            _modelBuilder.Entity<GiftIdea>().HasData(giftIdeas);
            _modelBuilder.Entity<Comment>().HasData(comments1);
            _modelBuilder.Entity<Category>().HasData(categories1);
            _modelBuilder.Entity<GiftIdeaCategory>().HasData(giftIdeaCategories1);
        }


    }
}



