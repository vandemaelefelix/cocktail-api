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

    public interface ICategoryRepository {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int categoryId);

        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(int categoryId, UpdateCategoryDTO category);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private ICocktailContext _context;

        public CategoryRepository(ICocktailContext context) {
            _context = context;
        }

        public async Task<List<Category>> GetCategories() {
            try
            {
                return await _context.Category.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public async Task<Category> GetCategory(int categoryId) {
            try
            {
                return await _context.Category.Where(c => c.CategoryId == categoryId).SingleOrDefaultAsync(); 
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public async Task<Category> AddCategory(Category category) {
            try
            {
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    
        public async Task<Category> UpdateCategory(int categoryId, UpdateCategoryDTO category) {
            try
            {
                var oldCategory = await _context.Category.Where(c => c.CategoryId == categoryId).SingleOrDefaultAsync();

                if (oldCategory != null) {
                    PropertyInfo[] properties = category.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(category, null) != null) {
                            var value = property.GetValue(category, null);
                            oldCategory.GetType().GetProperty(property.Name).SetValue(oldCategory, value, null);
                        }
                    }
                }

                _context.Category.Update(oldCategory);

                await _context.SaveChangesAsync();

                return oldCategory;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
    }
}
