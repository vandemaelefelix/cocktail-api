using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocktails.API.Models
{
    public class Cocktail
    {
        public Guid CocktailId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Alcoholic { get; set; }
        public List<CocktailCategory> CocktailCategories { get; set; }
        public List<CocktailIngredient> CocktailIngredients { get; set; }
        public List<CocktailImage> Images { get; set; }
    }
}
