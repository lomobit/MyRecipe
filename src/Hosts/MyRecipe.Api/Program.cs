using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.ComponentRegistrar;
using MyRecipe.Handlers;

// ������ ������ ��������, ��� ��� ����������� - ��� API-�����������
[assembly: ApiController]


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// ���������� ������������ ��� MyRecipe
builder.Services.AddMyRecipe();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
     options.WithOrigins("http://localhost:3000")
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod());

app.Run();
