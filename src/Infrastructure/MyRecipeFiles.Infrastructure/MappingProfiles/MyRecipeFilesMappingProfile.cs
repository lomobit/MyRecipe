using AutoMapper;
using MyRecipe.Contracts.File;

namespace MyRecipeFiles.Infrastructure.MappingProfiles;

public class MyRecipeFilesMappingProfile : Profile
{
    public MyRecipeFilesMappingProfile()
    {
        CreateMap<Domain.File, FileDto>();
    }
}