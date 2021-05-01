using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cocktails.API.Services;
using Cocktails.API.Models;
using Cocktails.API.DTO;

namespace Cocktails.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CocktailController : ControllerBase
    {
        private readonly ICocktailService _cocktailService;
        private readonly ILogger<CocktailController> _logger;

        public CocktailController(ILogger<CocktailController> logger, ICocktailService cocktailService)
        {
            _logger = logger;
            _cocktailService = cocktailService;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories() {
            // return "Yes it works";

            try
            {
                return new OkObjectResult(await _cocktailService.GetCategories());
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryId) {
            try
            {
                return new OkObjectResult(await _cocktailService.GetCategory(categoryId));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }


        [HttpGet]
        [Route("ingredients")]
        public async Task<ActionResult<List<Ingredient>>> GetIngredients() {
            try
            {
                return new OkObjectResult(await _cocktailService.GetIngredients());
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("ingredients/{ingredientId}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(Guid ingredientId) {
            try
            {
                return new OkObjectResult(await _cocktailService.GetIngredient(ingredientId));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);                
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("cocktails")]
        public async Task<ActionResult<List<Cocktail>>> GetCocktails() {
            try
            {
                return new OkObjectResult(await _cocktailService.GetCocktails());
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);                
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("cocktails/{cocktailId}")]
        public async Task<ActionResult<Cocktail>> GetCocktail(Guid cocktailId) {
            try
            {
                return new OkObjectResult(await _cocktailService.GetCocktail(cocktailId));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);                
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("cocktails")]
        public async Task<ActionResult<CocktailDTO>> AddCocktail(CocktailDTO cocktail)
        {
            try{
                return new OkObjectResult(await _cocktailService.AddCocktail(cocktail));
            }
            catch(Exception ex){
                Console.Write(ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("ingredients")]
        public async Task<ActionResult<AddIngredientDTO>> AddIngredient(AddIngredientDTO ingredient)
        {
            try{
                return new OkObjectResult(await _cocktailService.AddIngredient(ingredient));
            }
            catch(Exception ex){
                Console.Write(ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("categories")]
        public async Task<ActionResult<AddCategoryDTO>> AddCategory(AddCategoryDTO category)
        {
            try{
                return new OkObjectResult(await _cocktailService.AddCategory(category));
            }
            catch(Exception ex){
                Console.Write(ex);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("cocktails/{cocktailId}")]
        public async Task<ActionResult<Cocktail>> DeleteCocktail(Guid cocktailId)
        {
            try
            {
                return new OkObjectResult(await _cocktailService.DeleteCocktail(cocktailId));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        [HttpPut]
        [Route("cocktails/{cocktailId}")]
        public async Task<ActionResult<Cocktail>> UpdateCocktail(Guid cocktailId, CocktailUpdateDTO cocktail) 
        {
            try
            {
                return new OkObjectResult(await _cocktailService.UpdateCocktail(cocktailId, cocktail));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        [HttpPut]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<Category>> UpdateCategory(int categoryId, UpdateCategoryDTO category) 
        {
            try
            {
                return new OkObjectResult(await _cocktailService.UpdateCategory(categoryId, category));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }

        [HttpPut]
        [Route("ingredients/{ingredientId}")]
        public async Task<ActionResult<Ingredient>> UpdateIngredient(Guid ingredientId, UpdateIngredientDTO ingredient) 
        {
            try
            {
                return new OkObjectResult(await _cocktailService.UpdateIngredient(ingredientId, ingredient));
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
        }
    }
}
