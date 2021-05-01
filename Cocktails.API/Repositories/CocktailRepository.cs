using System.Reflection;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktails.API.DataContext;
using Cocktails.API.Models;
using Cocktails.API.Services;
using Microsoft.EntityFrameworkCore;
using Cocktails.API.DTO;
using AutoMapper;

namespace Cocktails.API.Repositories
{
    public interface ICocktailRepository
    {
        Task<Cocktail> AddCocktail(Cocktail cocktail);
        Task AddCocktailImage(CocktailImage cocktailImage);
        Task<Cocktail> GetCocktail(Guid cocktailId);
        Task<List<Cocktail>> GetCocktails();
        Task<Guid> DeleteCocktail(Guid cocktailId);
        Task<Cocktail> UpdateCocktail(Guid cocktailId, CocktailUpdateDTO cocktail);
    }
    public class CocktailRepository : ICocktailRepository
    {
        private ICocktailContext _context;
        private IBlobService _blobService;
        private IMapper _mapper;
        public CocktailRepository(ICocktailContext context, IBlobService blobService, IMapper mapper)
        {
            _context = context;
            _blobService = blobService;
            _mapper = mapper;
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

        public async Task<Guid> DeleteCocktail(Guid cocktailId) {
            var cocktail = await _context.Cocktails.Where(c => c.CocktailId == cocktailId)
            .Include(c => c.Images)
            .SingleOrDefaultAsync();

            _context.Cocktails.Remove(cocktail);

            foreach(var image in cocktail.Images) {
                await _context.SaveChangesAsync();
                await _blobService.DeleteBlob("cocktail-images", image.Name);
            }

            return cocktailId;
        }

        public async Task<Cocktail> UpdateCocktail(Guid cocktailId, CocktailUpdateDTO cocktail) {
            var oldCocktail = await _context.Cocktails.Where(c => c.CocktailId == cocktailId)
            .Include(c => c.CocktailCategories)
            .Include(c => c.CocktailIngredients)
            .Include(c => c.Images)
            .SingleOrDefaultAsync();

            if (oldCocktail != null) {
                PropertyInfo[] properties = cocktail.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetValue(cocktail, null) != null) {
                        var value = property.GetValue(cocktail, null);
                        oldCocktail.GetType().GetProperty(property.Name).SetValue(oldCocktail, value, null);
                    }
                }
            }

            _context.Cocktails.Update(oldCocktail);

            await _context.SaveChangesAsync();

            return oldCocktail;
        }
    }
}
