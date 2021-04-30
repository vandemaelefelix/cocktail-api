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
                return await _context.Ingredient.ToListAsync();
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
                // return await _context.Category.
                return await _context.Ingredient.Where(i => i.IngredientId == ingredientId).SingleOrDefaultAsync(); 
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
