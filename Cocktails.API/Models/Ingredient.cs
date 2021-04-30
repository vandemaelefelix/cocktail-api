using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocktails.API.Models
{
    public class Ingredient
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AlcoholPercentage { get; set; }

        [JsonIgnore]
        public List<CocktailIngredient> CocktailIngredients { get; set; }
    }
}
