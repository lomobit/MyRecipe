﻿
namespace MyRecipe.Contracts.Ingredient
{
    public class IngredientDto
    {
        /// <summary>
        /// Идентификатор ингридиента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование ингридиента.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Описание ингредиента.
        /// </summary>
        public string? Description { get; set; }
    }
}
