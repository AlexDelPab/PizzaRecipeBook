using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PizzaReceipeBook.Infrastructure;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.Author;

public static class CreateAuthor
{
    record RequestBody(string FirstName, string LastName, string Bio);

    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{ApiPath.Base}/authors", async (RequestBody body, IRequestProcessor processor) =>
        {
            var authorId = await processor.HandleAsync(new Command(body.FirstName, body.LastName, body.Bio), default);
            return Results.Ok(authorId);
        });
    }

    internal record Command(string FirstName, string LastName, string Bio) : ICommand<Guid>;

    internal sealed class Handler(AppDbContext dbContext) : IRequestHandler<Command, Guid>
    {
        public async Task<Guid> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var author = new Domain.Author(Guid.NewGuid(), request.FirstName, request.LastName, request.Bio);
            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}