using System;
using System.Text.Json.Serialization;

namespace Cocktails.API.Models
{
    public class IngredientImage
    {
        public Guid IngredientImageId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Guid IngredientId { get; set; }
    }
}
