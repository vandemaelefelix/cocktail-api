using System;
using System.Text.Json.Serialization;

namespace Cocktails.API.Models
{
    public class CocktailImage
    {
        public Guid CocktailImageId { get; set; }
        public string Name { get; set; }
        
        [JsonIgnore]
        public Guid CocktailId { get; set; }
    }
}
