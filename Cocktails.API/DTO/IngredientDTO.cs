using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cocktails.API.Models;

namespace Cocktails.API.DTO
{
    public class IngredientDTO
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AlcoholPercentage { get; set; }
    }
}
