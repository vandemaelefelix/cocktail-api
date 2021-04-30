using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.API.Models
{
    public class CocktailCategory
    {
        public Guid CocktailId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
