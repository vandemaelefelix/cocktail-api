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

        // [HttpPost]
        // [Route("cocktails")]
        // public async Task<ActionResult<CocktailDTO>> AddCategory(CocktailDTO cocktail)
        // {
        //     try{
        //         return new OkObjectResult(await _cocktailService.AddCocktail(cocktail));
        //     }
        //     catch(Exception ex){
        //         Console.Write(ex);
        //         return new StatusCodeResult(500);
        //     }
        // }

    }
}
