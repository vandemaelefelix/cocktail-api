using System;
using AutoMapper;
using Cocktails.API.Models;

namespace Cocktails.API.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping() {
            CreateMap<Category, CategoryDTO>();           
            CreateMap<Ingredient, IngredientDTO>();           
            CreateMap<CocktailDTO, Cocktail>();           
        }
    }
}
