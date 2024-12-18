﻿namespace Catalog.API.Products.GetProductById;

public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler
    (IDocumentSession session,ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);
        var product = await session.Query<Product>()
            .Where(a => a.Category.Contains(query.Category)).ToListAsync();
        if(product == null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByCategoryResult(product);
    }

}
