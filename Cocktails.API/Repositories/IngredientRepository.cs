using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktails.API.DataContext;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.Repositories
{
    public interface IIngredientRepository {
        Task<List<Ingredient>> GetIngredients();
        Task<Ingredient> GetIngredient(Guid ingredientId);

        Task<Ingredient> AddIngredient(Ingredient ingredient);
        Task AddIngredientImage(IngredientImage ingredientImage);
        
    }
    public class IngredientRepository : IIngredientRepository
    {
        private ICocktailContext _context;

        public IngredientRepository(ICocktailContext context) {
            _context = context;
        }

        public async Task<List<Ingredient>> GetIngredients() {
            try
            {
                return await _context.Ingredients
                .Include(i => i.Images)
                .ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        public async Task<Ingredient> GetIngredient(Guid ingredientId) {
            try
            {
                return await _context.Ingredients.Where(i => i.IngredientId == ingredientId)
                .Include(i => i.Images)
                .SingleOrDefaultAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task AddIngredientImage(IngredientImage ingredientImage)
        {
            await _context.IngredientImages.AddAsync(ingredientImage);
            await _context.SaveChangesAsync();
        }

        public async Task<Ingredient> AddIngredient(Ingredient ingredient) {
            try
            {
                await _context.Ingredients.AddAsync(ingredient);
                await _context.SaveChangesAsync();
                return ingredient;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}
