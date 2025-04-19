using Microsoft.AspNetCore.Mvc;
using PizzaReceipeBook.Application;
using PizzaReceipeBook.Application.Requests.Author;
using PizzaReceipeBook.Application.Services.Author;

namespace PizzaReceipeBook.Host.Controllers;

public static class AuthorController
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup($"{ApiPath.Base}/authors");
        MapCreateAuthor(group);
        MapGetAuthorByName(group);
        MapGetAllAuthorsForDropdownSelect(group);
    }

    private static RouteHandlerBuilder MapCreateAuthor(IEndpointRouteBuilder app)
    {
        return app.MapPost("", async ([FromBody] CreateAuthorRequest request, AuthorService service) =>
        {
            var authorId = await service.CreateAuthorAsync(request.FirstName, request.LastName, request.Bio);
            return Results.Ok(authorId);
        });
    }

    private static RouteHandlerBuilder MapGetAuthorByName(IEndpointRouteBuilder app)
    {
        return app.MapGet("/by-name",
            async ([FromQuery] string name, AuthorService service) =>
            {
                var author = await service.GetAuthorByNameAsync(name);
                return Results.Ok(author);
            });
    }

    private static RouteHandlerBuilder MapGetAllAuthorsForDropdownSelect(IEndpointRouteBuilder app)
    {
        return app.MapGet("/for-dropdown-select",
            async (AuthorService authorService) =>
            {
                var authors = await authorService.GetAllAuthorsForDropdownSelectAsync();
                return Results.Ok(authors);
            });
    }
}