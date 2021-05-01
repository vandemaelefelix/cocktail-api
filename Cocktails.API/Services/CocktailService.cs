using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cocktails.API.DTO;
using Cocktails.API.Models;
using Cocktails.API.Repositories;

namespace Cocktails.API.Services
{

    public interface ICocktailService
    {
        Task<List<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategory(int categoryId);
        Task<AddCategoryDTO> AddCategory(AddCategoryDTO category);
        Task<List<IngredientDTO>> GetIngredients();
        Task<IngredientDTO> GetIngredient(Guid ingredientId);

        Task<Cocktail> GetCocktail(Guid cocktailId);
        Task<List<Cocktail>> GetCocktails();
        Task<CocktailDTO> AddCocktail(CocktailDTO cocktail);
        Task<AddIngredientDTO> AddIngredient(AddIngredientDTO ingredient);
    }
    public class CocktailService : ICocktailService
    {
        private IBlobService _blobService;
        private IMapper _mapper;
        private ICategoryRepository _categoryRepository;
        private IIngredientRepository _ingredientRepository;
        private ICocktailRepository _cocktailRepository;

        public CocktailService(IMapper mapper, ICategoryRepository categoryRepository, IIngredientRepository ingredientRepository, ICocktailRepository cocktailRepository, IBlobService blobService) {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
            _cocktailRepository = cocktailRepository;
            _blobService = blobService;
        }

        public async Task<List<CategoryDTO>> GetCategories() {
            return _mapper.Map<List<CategoryDTO>>(await _categoryRepository.GetCategories());
        }

        public async Task<CategoryDTO> GetCategory(int categoryId) {
            return _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategory(categoryId));
        }
        public async Task<List<IngredientDTO>> GetIngredients() {
            return _mapper.Map<List<IngredientDTO>>(await _ingredientRepository.GetIngredients());
        }
        public async Task<IngredientDTO> GetIngredient(Guid ingredientId) {
            return _mapper.Map<IngredientDTO>(await _ingredientRepository.GetIngredient(ingredientId));
        }

        public async Task<Cocktail> GetCocktail(Guid cocktailId) {
            try
            {
                return await _cocktailRepository.GetCocktail(cocktailId);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        public async Task<List<Cocktail>> GetCocktails() {
            try
            {
                return await _cocktailRepository.GetCocktails();
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        public async Task<CocktailDTO> AddCocktail(CocktailDTO cocktail) {
            try
            {
                string containerName = "cocktail-images";

                Cocktail newCocktail = _mapper.Map<Cocktail>(cocktail);
                
                newCocktail.CocktailCategories = new List<CocktailCategory>();
                foreach (var categoryId in cocktail.Categories) {
                    newCocktail.CocktailCategories.Add(new CocktailCategory() {CategoryId = categoryId});
                }

                newCocktail.CocktailIngredients = new List<CocktailIngredient>();
                foreach (var ingredientId in cocktail.Ingredients) {
                    newCocktail.CocktailIngredients.Add(new CocktailIngredient() {IngredientId = ingredientId});
                }

                await _cocktailRepository.AddCocktail(newCocktail);

                for (int i = 0; i < cocktail.ImageEncoded.Count; i++) {
                    byte[] bytes = System.Convert.FromBase64String(cocktail.ImageEncoded[i]);
                    string fileName = $"{Guid.NewGuid()}.{cocktail.Extension[i]}";
                    await _blobService.UploadByteArray(containerName, bytes, fileName);
                    await _cocktailRepository.AddCocktailImage(new CocktailImage() { CocktailId = newCocktail.CocktailId, Name = fileName });
                }

                return cocktail;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddIngredientDTO> AddIngredient(AddIngredientDTO ingredient) {
            try
            {
                string containerName = "ingredient-images";

                Ingredient newIngredient = _mapper.Map<Ingredient>(ingredient);

                await _ingredientRepository.AddIngredient(newIngredient);

                for (int i = 0; i < ingredient.EncodedImages.Count; i++) {
                    byte[] bytes = System.Convert.FromBase64String(ingredient.EncodedImages[i]);
                    string fileName = $"{Guid.NewGuid()}.{ingredient.Extensions[i]}";
                    await _blobService.UploadByteArray(containerName, bytes, fileName);
                    await _ingredientRepository.AddIngredientImage(new IngredientImage() { IngredientId = newIngredient.IngredientId, Name = fileName });
                }

                return ingredient;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddCategoryDTO> AddCategory(AddCategoryDTO category) {
            try
            {
                Category newCategory = _mapper.Map<Category>(category);
                await _categoryRepository.AddCategory(newCategory);

                return category;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
