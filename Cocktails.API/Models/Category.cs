using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocktails.API.Models
{
    public class Category
    {
        // [KeyAttribute]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<CocktailCategory> CocktailCategories { get; set; }
    }
}
