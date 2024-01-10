using Client.Application.Contracts.Persistence;
using Client.Application.Contracts.Specs;
using Client.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Client.Persistence.Repositories;
public class ClientRepository : GenericRepository<Domain.Client>, IClientRepository
{
    private readonly ClientDbContext _dbContext;

    public ClientRepository(ClientDbContext dbContext) : base(dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<bool> CinExist(string cin, CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.AnyAsync(o => o.Cin == cin, cancellationToken);
    }

    public async Task<bool> ClientCinExist(Guid clientId, string cin, CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.AnyAsync(o => o.Cin == cin && o.Id == clientId, cancellationToken);
    }

    public async Task<Pagination<Domain.Client>> GetClientsAsync(SpecParams specParams, CancellationToken cancellationToken)
    {
        var queryableClients = _dbContext.Clients.AsQueryable();
        var filterExpression = BuildFilterExpression(specParams);
        queryableClients = queryableClients
                             .Where(filterExpression)
                             .OrderBy(SortExpression(specParams.SortBy,specParams.SortOrder))
                             .Skip((specParams.PageIndex - 1) * specParams.PageSize)
                             .Take(specParams.PageSize);

        var data = await queryableClients
                             .ToListAsync(cancellationToken);

        var totalRecords = await _dbContext.Clients.CountAsync(cancellationToken);

        return new Pagination<Domain.Client>(specParams.PageIndex, specParams.PageSize, totalRecords, data);
    }

    public Expression<Func<Domain.Client, bool>> BuildFilterExpression(SpecParams specParams)
    {
        if (specParams.Search == null || !specParams.Search.Any())
            return c => true;
        var parameter = Expression.Parameter(typeof(Domain.Client), "c");
        var filterExpressions = specParams.Search
            .Select(field => BuildFilterExpressionForField(parameter, field.Property, field.Value))
            .ToList();
        var combinedExpression = filterExpressions.Aggregate(Expression.AndAlso);
        return Expression.Lambda<Func<Domain.Client, bool>>(combinedExpression, parameter);
    }

    private Expression BuildFilterExpressionForField(ParameterExpression parameter, string field, string searchTerm)
    {
        switch (field)
        {
            case "Name":
                return Expression.Call(Expression.Property(parameter, "Name"), "Contains", null, Expression.Constant(searchTerm));
            case "LastName":
                return Expression.Call(Expression.Property(parameter, "LastName"), "Contains", null, Expression.Constant(searchTerm));
            case "Cin":
                return Expression.Call(Expression.Property(parameter, "Cin"), "Contains", null, Expression.Constant(searchTerm));
            case "PhoneNumber":
                return Expression.Call(Expression.Property(parameter, "PhoneNumber"), "Contains", null, Expression.Constant(searchTerm));
            case "Address":
                return Expression.Call(Expression.Property(parameter, "Address"), "Contains", null, Expression.Constant(searchTerm));
            default:
                return Expression.Constant(true);
        }
    }
    private string SortExpression(string sortField, string sortOrder)
    {
        if (string.IsNullOrEmpty(sortField))
            return "";
        switch (sortField)
        {
            case "Name":
                return string.Join(" ", new String[] { "Name", sortOrder });
            case "LastName":
                return string.Join(" ", new String[] { "LastName", sortOrder });
            case "Cin":
                return string.Join(" ", new String[] { "Cin", sortOrder });
            case "PhoneNumber":
                return string.Join(" ", new String[] { "PhoneNumber", sortOrder });
            case "Address":
                return string.Join(" ", new String[] { "Address", sortOrder });
            default:
                return "";
        }
    }
}
