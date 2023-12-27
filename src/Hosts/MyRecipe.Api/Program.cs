using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.ComponentRegistrar;
using MyRecipe.Handlers;
using MyRecipeFiles.ComponentRegistrar;
using MyRecipeFiles.Handlers;
using MyRecipeLogging.ComponentRegistrar;

// Задаем сборке аттрибут, что все контроллеры - это API-контроллеры
[assembly: ApiController]


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);
builder.Services.AddMediatR(typeof(MediatREntrypointFiles).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// Добавление зависимостей для MyRecipe
builder.Services.AddMyRecipe();
builder.Services.AddMyRecipeLogging();
builder.Services.AddMyRecipeFiles();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment == null)
{
    throw new ArgumentNullException(nameof(app.Environment));
}

if (app.Environment.IsEnvironment(Environments.Development) || app.Environment.IsEnvironment(Environments.Staging))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000")
        .WithOrigins("https://localhost:3001")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Https over Docker
// https://github.com/dotnet/dotnet-docker/blob/main/samples/run-aspnetcore-https-development.md
app.Run();