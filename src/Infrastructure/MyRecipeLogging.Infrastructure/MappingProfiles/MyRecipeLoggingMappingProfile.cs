using AutoMapper;
using MyRecipeLogging.Contracts.Log;
using MyRecipeLogging.Domain;

namespace MyRecipeLogging.Infrastructure.MappingProfiles
{
    public class MyRecipeLoggingMappingProfile : Profile
    {
        public MyRecipeLoggingMappingProfile()
        {
            CreateMap<Log, LogDto>();
        }
    }
}
