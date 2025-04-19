using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PizzaReceipeBook.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using softaware.Cqs;

namespace PizzaReceipeBook.Application.Features.Author;

public static class GetAllAuthorsForDropdownSelect
{
    public static RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{ApiPath.Base}/authors/for-dropdown-select",
            async (IRequestProcessor processor) =>
            {
                var authors = await processor.HandleAsync(new Query(), default);
                return Results.Ok(authors);
            });
    }

    public class Query : IQuery<IEnumerable<AuthorDto>>;

    public class Handler(AppDbContext dbContext) : IRequestHandler<Query, IEnumerable<AuthorDto>>
    {
        public async Task<IEnumerable<AuthorDto>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            return await dbContext.Authors
                .Select(a => new AuthorDto(a.Id, $"{a.FirstName} {a.LastName}"))
                .ToListAsync(cancellationToken);
        }
    }

    public record AuthorDto(Guid Id, string FullName);
}