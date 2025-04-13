using Carter;
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
    class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiPath.Base}/authors/by-name", async ([FromQuery] string name, IRequestProcessor processor) =>
            {
                var authorId = await processor.HandleAsync(new Query(name), default);
                return Results.Ok(authorId);
            });
        }
    }

    internal record Query(string Name) : IQuery<AuthorResponse?>;

    internal sealed class Handler : IRequestHandler<Query, AuthorResponse?>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthorResponse?> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var author = await _dbContext.Authors
                .Where(a => EF.Functions.Like(a.FirstName, $"%{request.Name}%") ||
                            EF.Functions.Like(a.LastName, $"%{request.Name}%"))
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

    internal record AuthorResponse(Guid Id, string FirstName, string LastName, string Bio);
}