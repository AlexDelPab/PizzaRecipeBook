using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PizzaReceipeBook.Infrastructure;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.PizzaRecipe;

public static class UpdatePizzaRecipeName
{
    record RequestBody(string Name);

    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPut($"{ApiPath.Base}/pizza-recipes/{{id:guid}}/name", async (Guid id, RequestBody body, IRequestProcessor processor) =>
        {
            await processor.HandleAsync(new Command(id, body.Name), default);
            return Results.Ok();
        });
    }

    internal record Command(Guid Id, string Name) : ICommand<NoResult>;

    internal sealed class Handler(AppDbContext dbContext) : IRequestHandler<Command, NoResult>
    {
        public async Task<NoResult> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var pizzaRecipe = await dbContext.PizzaRecipes.FindAsync(request.Id, cancellationToken);

            if (pizzaRecipe == null)
            {
                return NoResult.Value;
            }

            pizzaRecipe.Name = request.Name;
            await dbContext.SaveChangesAsync(cancellationToken);

            return NoResult.Value;
        }
    }
}