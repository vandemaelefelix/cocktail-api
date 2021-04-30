using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktails.API.DataContext;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.Repositories
{

    public interface ICategoryRepository {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int categoryId);
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
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Category> GetCategory(int categoryId) {
            try
            {
                // return await _context.Category.
                return await _context.Category.Where(c => c.CategoryId == categoryId).SingleOrDefaultAsync(); 
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}
