using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.API.DTO
{
    public class CocktailDTO
    {
        [Required(ErrorMessage = "Please provide a name for your cocktail.")]
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        [StringLength(255, ErrorMessage = "Description can be maximum 255 characters long.")]
        public string Description { get; set; }
        
        public List<int> Categories { get; set; }
        public List<Guid> Ingredients { get; set; }

        public List<string> ImageEncoded { get; set; }
        public List<string> Extension { get; set; }
    }

    public class CocktailUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Alcoholic { get; set; }
    }

    public class AddCocktailImagesDTO
    {
        public List<string> EncodedImages { get; set; }
        public List<string> Extensions { get; set; }
    }
}
