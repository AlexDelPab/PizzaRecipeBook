using Carter;
using Microsoft.EntityFrameworkCore;
using PizzaReceipeBook.Application;
using PizzaReceipeBook.Domain;
using PizzaReceipeBook.Infrastructure;
using softaware.Cqs.Decorators.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        c => c.MigrationsAssembly(typeof(AppDbContext).Assembly)));

builder.Services
    .AddSoftawareCqs(b => b.IncludeTypesFrom(typeof(ApiPath).Assembly))
    .AddDecorators(b => b.AddRequestHandlerDecorator(typeof(ValidationRequestHandlerDecorator<,>)));

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    var authors = db.Authors.ToList();
    if (!authors.Any())
    {
        db.Add(new Author(Guid.NewGuid(), "Alex", "Pabinger", "Loves Pizza, but can't make it!"));
        db.SaveChanges();
    }
}

app.MapCarter();

app.Run();