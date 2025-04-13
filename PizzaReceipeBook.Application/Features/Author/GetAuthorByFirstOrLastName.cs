using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaReceipeBook.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.Author;

public static class GetAuthorByFirstOrLastName
{
    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{ApiPath.Base}/authors/by-name",
            async ([FromQuery] string name, IRequestProcessor processor) =>
            {
                var authorId = await processor.HandleAsync(new Query(name), default);
                return Results.Ok(authorId);
            });
    }

    public class Query(string name) : IQuery<AuthorResponse?>
    {
        public string Name { get; } = name;
    }

    public class Handler(AppDbContext dbContext) : IRequestHandler<Query, AuthorResponse?>
    {
        public async Task<AuthorResponse?> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var author = await dbContext.Authors
                .Where(a => a.FirstName.Contains(request.Name) || a.LastName.Contains(request.Name))
                .ToListAsync(cancellationToken);

            if (author.Count > 1)
            {
                throw new InvalidOperationException("Multiple authors found with the given name.");
            }

            if (!author.Any())
            {
                return null;
            }

            var result = author.Single();
            return new AuthorResponse(result.Id, result.FirstName, result.LastName, result.Bio);
        }
    }

    public record AuthorResponse(Guid Id, string FirstName, string LastName, string Bio);
}