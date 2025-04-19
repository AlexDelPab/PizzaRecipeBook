using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PizzaReceipeBook.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.PizzaRecipe;

public static class GetAllPizzaRecipesForListPage
{
    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{ApiPath.Base}/pizza-recipes/for-list-page",
            async (IRequestProcessor processor) =>
            {
                var pizzaRecipes = await processor.HandleAsync(new Query(), default);
                return Results.Ok(pizzaRecipes);
            });
    }

    public class Query : IQuery<IEnumerable<PizzaRecipeDto>>;

    public class Handler(AppDbContext dbContext) : IRequestHandler<Query, IEnumerable<PizzaRecipeDto>>
    {
        public async Task<IEnumerable<PizzaRecipeDto>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            return await dbContext.PizzaRecipes
                .Select(p => new PizzaRecipeDto(p.Id, p.Name, p.Ingredients, p.Instructions))
                .ToListAsync(cancellationToken);
        }
    }

    public record PizzaRecipeDto(Guid Id, string Name, string Ingredients, string Instructions);
}