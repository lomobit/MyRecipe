
using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Ingredient;
using System.ComponentModel.DataAnnotations;
using MyRecipe.Handlers.Contracts.Ingredient;

namespace MyRecipe.Handlers.Ingredient
{
    public class IngredientGetQueryBehavior : IPipelineBehavior<IngredientGetQuery, Pagination<IngredientDto>>
    {
        private readonly ILogger _logger;

        public IngredientGetQueryBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Pagination<IngredientDto>> Handle(IngredientGetQuery request, RequestHandlerDelegate<Pagination<IngredientDto>> next, CancellationToken cancellationToken)
        {
            if (request == null || request.PageNumber < 1 || request.PageSize < 1)
            {
                _logger.LogError($"Некорректный запрос {nameof(IngredientGetQuery)} в {nameof(IngredientGetQueryBehavior)}");
                
                var ex = new ValidationException();
                ex.Data.Add("Ошибка", "Некорректный запрос");

                throw ex;
            }

            return await next();
        }
    }
}
