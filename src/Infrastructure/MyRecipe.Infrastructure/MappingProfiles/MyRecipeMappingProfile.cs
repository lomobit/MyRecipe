using AutoMapper;
using MyRecipe.Contracts.Dish;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Contracts.IngredientsForDish;
using MyRecipe.Contracts.Okei;
using MyRecipe.Domain;

namespace MyRecipe.Infrastructure.MappingProfiles
{
    public class MyRecipeMappingProfile : Profile
    {
        public MyRecipeMappingProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            
            CreateMap<Dish, DishForGridDto>();
            CreateMap<Dish, DishDto>();

            CreateMap<IngredientsForDish, IngredientsForDishDto>();

            CreateMap<Okei, OkeiDto>();
        }
    }
}
