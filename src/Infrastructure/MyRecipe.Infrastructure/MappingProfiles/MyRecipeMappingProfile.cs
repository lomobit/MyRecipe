
using AutoMapper;
using MyRecipe.Contracts.Ingredient;
using MyRecipe.Domain;

namespace MyRecipe.Infrastructure.MappingProfiles
{
    public class MyRecipeMappingProfile : Profile
    {
        public MyRecipeMappingProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
        }
    }
}
