using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaReceipeBook.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.Author;

public static class GetAuthorByName
{
    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{ApiPath.Base}/authors/by-name",
            async ([FromQuery] string name, IRequestProcessor processor) =>
            {
                var author = await processor.HandleAsync(new Query(name), default);
                return Results.Ok(author);
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
                .Where(a => EF.Functions.Like(a.FirstName.ToLower(), $"%{request.Name.ToLower()}%") || 
                            EF.Functions.Like(a.LastName.ToLower(), $"%{request.Name.ToLower()}%"))
                .SingleOrDefaultAsync(cancellationToken);

            return author is null ? null : new AuthorResponse(author.Id, author.FirstName, author.LastName);
        }
    }

    public record AuthorResponse(Guid Id, string FirstName, string LastName);
}