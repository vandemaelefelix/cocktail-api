using System;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.API.Models
{
    public class CocktailIngredient
    {
        public Guid CocktailId { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
