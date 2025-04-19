using Microsoft.AspNetCore.Routing;
using PizzaReceipeBook.Application.Features.Author;
using PizzaReceipeBook.Application.Features.PizzaRecipe;

namespace PizzaReceipeBook.Application.Features;

public static class MapFeatureEndpoints
{
    public static void Map(IEndpointRouteBuilder endpoints)
    {
        // Author
        CreateAuthor.MapEndpoint(endpoints);
        GetAuthorByName.MapEndpoint(endpoints);
        GetAllAuthorsForDropdownSelect.MapEndpoint(endpoints);
        
        // Pizza Recipes
        UpdatePizzaRecipeName.MapEndpoint(endpoints);
        GetAllPizzaRecipesForListPage.MapEndpoint(endpoints);
    }
}