using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cocktails.API.Models;

namespace Cocktails.API.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // [JsonIgnore]
        // public List<CocktailCategory> CocktailCategories { get; set; }
    }

    public class AddCategoryDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryDTO
    {
        public string Name { get; set; }
    }
}
