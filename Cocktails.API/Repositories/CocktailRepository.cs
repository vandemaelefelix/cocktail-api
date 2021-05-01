using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktails.API.DataContext;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.Repositories
{
    public interface ICocktailRepository
    {
        Task<Cocktail> AddCocktail(Cocktail cocktail);
        Task AddCocktailImage(CocktailImage cocktailImage);
        Task<Cocktail> GetCocktail(Guid cocktailId);
        Task<List<Cocktail>> GetCocktails();
    }
    public class CocktailRepository : ICocktailRepository
    {
        private ICocktailContext _context;
        public CocktailRepository(ICocktailContext context)
        {
            _context = context;
        }

        public async Task<Cocktail> AddCocktail(Cocktail cocktail) {
            try
            {
                await _context.Cocktails.AddAsync(cocktail);
                await _context.SaveChangesAsync();
                return cocktail;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        public async Task AddCocktailImage(CocktailImage cocktailImage)
        {
            await _context.CocktailImages.AddAsync(cocktailImage);
            await _context.SaveChangesAsync();
        }

        public async Task<Cocktail> GetCocktail(Guid cocktailId) {
            return await _context.Cocktails.Where(c => c.CocktailId == cocktailId)
            .Include(c => c.CocktailCategories)
            .ThenInclude(c => c.Category)
            .Include(c => c.CocktailIngredients)
            .ThenInclude(c => c.Ingredient)
            .ThenInclude(i => i.Images)
            .Include(c => c.Images)
            .SingleOrDefaultAsync();
        }

        public async Task<List<Cocktail>> GetCocktails() {
            return await _context.Cocktails
            .Include(c => c.CocktailCategories)
            .ThenInclude(c => c.Category)
            .Include(c => c.CocktailIngredients)
            .ThenInclude(c => c.Ingredient)
            .ThenInclude(i => i.Images)
            .Include(c => c.Images)
            .ToListAsync();
        }
    }

}
