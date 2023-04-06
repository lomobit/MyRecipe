﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyRecipe.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    [DbContext(typeof(MyRecipeDbContext))]
    [Migration("20230406065428_AddDishLinksForIngredientsForDish")]
    partial class AddDishLinksForIngredientsForDish
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DishMeal", b =>
                {
                    b.Property<int>("DishesId")
                        .HasColumnType("integer");

                    b.Property<int>("MealId")
                        .HasColumnType("integer");

                    b.HasKey("DishesId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("DishMeal");
                });

            modelBuilder.Entity("MyRecipe.Domain.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfPersons")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("MyRecipe.Domain.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("MyRecipe.Domain.IngredientsForDish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Condition")
                        .HasColumnType("text");

                    b.Property<int>("DishId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<string>("OkeiCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("OkeiCode");

                    b.ToTable("IngredientsForDishes");
                });

            modelBuilder.Entity("MyRecipe.Domain.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("MealScheduleId")
                        .HasColumnType("integer");

                    b.Property<int>("MealType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("MealScheduleId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("MyRecipe.Domain.MealSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MealSchedules");
                });

            modelBuilder.Entity("MyRecipe.Domain.Okei", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CodeDesignationInternational")
                        .HasColumnType("text");

                    b.Property<string>("CodeDesignationNational")
                        .HasColumnType("text");

                    b.Property<string>("ConventionDesignationInternational")
                        .HasColumnType("text");

                    b.Property<string>("ConventionDesignationNational")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.ToTable("Okeis");
                });

            modelBuilder.Entity("DishMeal", b =>
                {
                    b.HasOne("MyRecipe.Domain.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyRecipe.Domain.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyRecipe.Domain.IngredientsForDish", b =>
                {
                    b.HasOne("MyRecipe.Domain.Dish", "Dish")
                        .WithMany("IngredientsForDish")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyRecipe.Domain.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyRecipe.Domain.Okei", "Okei")
                        .WithMany()
                        .HasForeignKey("OkeiCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Ingredient");

                    b.Navigation("Okei");
                });

            modelBuilder.Entity("MyRecipe.Domain.Meal", b =>
                {
                    b.HasOne("MyRecipe.Domain.MealSchedule", null)
                        .WithMany("Meals")
                        .HasForeignKey("MealScheduleId");
                });

            modelBuilder.Entity("MyRecipe.Domain.Dish", b =>
                {
                    b.Navigation("IngredientsForDish");
                });

            modelBuilder.Entity("MyRecipe.Domain.MealSchedule", b =>
                {
                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
