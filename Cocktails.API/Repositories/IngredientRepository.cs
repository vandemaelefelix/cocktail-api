using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cocktails.API.DataContext;
using Cocktails.API.DTO;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.Repositories
{
    public interface IIngredientRepository {
        Task<List<Ingredient>> GetIngredients();
        Task<Ingredient> GetIngredient(Guid ingredientId);
        Task<Ingredient> AddIngredient(Ingredient ingredient);
        Task AddIngredientImage(IngredientImage ingredientImage);
        Task<Ingredient> UpdateIngredient(Guid ingredientId, UpdateIngredientDTO ingredient);
        
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
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
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

        public async Task<Ingredient> UpdateIngredient(Guid ingredientId, UpdateIngredientDTO ingredient) {
            try
            {
                var oldIngredient = await _context.Ingredients.Where(c => c.IngredientId == ingredientId).SingleOrDefaultAsync();

                if (oldIngredient != null) {
                    PropertyInfo[] properties = ingredient.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        var value = property.GetValue(ingredient, null);

                        if (property.PropertyType == typeof(Nullable<int>)) {
                            if (value != null) {
                                oldIngredient.GetType().GetProperty(property.Name).SetValue(oldIngredient, value, null);
                            } else {
                                continue;
                            }
                        } else {
                                oldIngredient.GetType().GetProperty(property.Name).SetValue(oldIngredient, value, null);
                        }
                    }
                }

                _context.Ingredients.Update(oldIngredient);

                await _context.SaveChangesAsync();

                return oldIngredient;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}
