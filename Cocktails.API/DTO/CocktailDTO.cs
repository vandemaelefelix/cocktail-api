using System;
using System.Collections.Generic;

namespace Cocktails.API.DTO
{
    public class CocktailDTO
    {
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        public string Description { get; set; }
        
        public List<int> Categories { get; set; }
        public List<Guid> Ingredients { get; set; }
    }
}