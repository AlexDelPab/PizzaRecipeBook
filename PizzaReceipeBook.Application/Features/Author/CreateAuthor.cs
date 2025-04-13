using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PizzaReceipeBook.Infrastructure;
using Microsoft.AspNetCore.Routing;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.Author;

public static class CreateAuthor
{
    record RequestBody(string FirstName, string LastName, string Bio);

    class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiPath.Base}/authors", async (RequestBody body, IRequestProcessor processor) =>
            {
                var authorId = await processor.HandleAsync(new Command(body.FirstName, body.LastName, body.Bio), default);
                return Results.Ok(authorId);
            });
        }
    }

    internal record Command(string FirstName, string LastName, string Bio) : ICommand<Guid>;

    internal sealed class Handler : IRequestHandler<Command, Guid>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var author = new Domain.Author(Guid.NewGuid(), request.FirstName, request.LastName, request.Bio);
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}