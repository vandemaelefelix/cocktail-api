using System;
using System.Threading.Tasks;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Cocktails.API.Config;
using System.Threading;

namespace Cocktails.API.DataContext

{
    public interface ICocktailContext
    {
        DbSet<Category> Category { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Cocktail> Cocktails { get; set; }
        DbSet<CocktailImage> CocktailImages { get; set; }
        DbSet<IngredientImage> IngredientImages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CocktailContext : DbContext, ICocktailContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailCategory> CocktailCategories { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<CocktailImage> CocktailImages { get; set; }
        public DbSet<IngredientImage> IngredientImages { get; set; }
        private ConnectionStrings _connectionStrings;

        public CocktailContext(DbContextOptions<CocktailContext> options, IOptions<ConnectionStrings> connectionStrings): base(options)
        {
            _connectionStrings = connectionStrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CocktailCategory>().HasKey(cc => new { cc.CocktailId, cc.CategoryId });
            modelBuilder.Entity<CocktailIngredient>().HasKey(ci => new { ci.CocktailId, ci.IngredientId });

            modelBuilder.Entity<Category>().HasData(new Category()
            {
                CategoryId = 1,
                Name = "Old fashioned"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                CategoryId = 2,
                Name = "Classic"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                CategoryId = 3,
                Name = "Strong"
            });

            // modelBuilder.Entity<Cocktail>().HasData(new Cocktail()
            // {
            //     CocktailId = new Guid(),
            //     Name = "Test Cocktail",
            //     Alcoholic = true
            // });


            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = Guid.NewGuid(),
                Name = "Rum",
                Description = "Alcoholic substance drunk by pirates",
                AlcoholPercentage = 41
            });
            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = Guid.NewGuid(),
                Name = "Vodka",
                Description = "Alcoholic substance drunk by Russians",
                AlcoholPercentage = 39
            });
            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = Guid.NewGuid(),
                Name = "Gin",
                Description = "Alcoholic substance drunk by Alcoholics",
                AlcoholPercentage = 38
            });
        }
    }
}
