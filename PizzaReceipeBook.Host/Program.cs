using Microsoft.EntityFrameworkCore;
using PizzaReceipeBook.Application.Services.Author;
using PizzaReceipeBook.Domain;
using PizzaReceipeBook.Host.Controllers;
using PizzaReceipeBook.Infrastructure;
using PizzaReceipeBook.Infrastructure.Author;
using PizzaReceipeBook.Infrastructure.Contracts.Author;
using PizzaReceipeBook.Infrastructure.Contracts.PizzaReceipe;
using PizzaReceipeBook.Infrastructure.PizzaReceipe;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PizzaReceipes"));

builder.Services.AddTransient<IAuthorRepository<Author>, AuthorRepository>();
builder.Services.AddTransient<IPizzaReceipeRepository<PizzaRecipe>, PizzaRecipeRepository>();
builder.Services.AddTransient<AuthorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

AuthorController.MapEndpoints(app);

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Seed database");
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var authors = db.Authors.ToList();
    var author = authors.FirstOrDefault();
    if (author is null)
    {
        Console.WriteLine("No author was found");
        author = new Author(Guid.NewGuid(), "Alex", "Pabinger", "Loves Pizza, but can't make it!");
        db.Add(author);
        db.SaveChanges();
    }

    if (!db.PizzaRecipes.Any())
    {
        Console.WriteLine("No pizza receipes found");
        db.AddRange(new PizzaRecipe(
            Guid.NewGuid(),
            "Margherita Pizza",
            "Dough, Tomato Sauce, Fresh Mozzarella Cheese, Fresh Basil, Olive Oil, Salt",
            "1. Preheat oven to 475°F (245°C).\n" +
            "2. Roll out pizza dough onto a floured surface.\n" +
            "3. Spread tomato sauce evenly on the dough.\n" +
            "4. Add slices of fresh mozzarella cheese.\n" +
            "5. Bake in the oven for 10 minutes.\n" +
            "6. Garnish with fresh basil, drizzle olive oil, and serve hot.",
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(10),
            author.Id,
            author
        ), new PizzaRecipe(
            Guid.NewGuid(),
            "Pepperoni Pizza",
            "Dough, Tomato Sauce, Mozzarella Cheese, Pepperoni Slices, Oregano",
            "1. Preheat oven to 475°F (245°C).\n" +
            "2. Roll out pizza dough to the desired thickness.\n" +
            "3. Spread tomato sauce over the dough evenly.\n" +
            "4. Sprinkle shredded mozzarella cheese.\n" +
            "5. Add pepperoni slices and sprinkle oregano on top.\n" +
            "6. Bake for 12 minutes until crust is brown and cheese is bubbling.\n" +
            "7. Let it cool for 2 minutes, then slice and serve.",
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(12),
            author.Id,
            author
        ), new PizzaRecipe(
            Guid.NewGuid(),
            "BBQ Chicken Pizza",
            "Dough, BBQ Sauce, Cooked Chicken Breast, Red Onion, Mozzarella Cheese, Cilantro",
            "1. Preheat oven to 475°F (245°C).\n" +
            "2. Roll out pizza dough and place it on a baking sheet.\n" +
            "3. Spread BBQ sauce over the dough.\n" +
            "4. Add shredded cooked chicken breast, sliced red onion, and mozzarella cheese.\n" +
            "5. Bake in the oven for 10-12 minutes until crust is crispy and cheese is melted.\n" +
            "6. Garnish with chopped cilantro before serving.",
            TimeSpan.FromMinutes(20),
            TimeSpan.FromMinutes(12),
            author.Id,
            author
        ));
        
        db.SaveChanges();
    }
}

app.Run();