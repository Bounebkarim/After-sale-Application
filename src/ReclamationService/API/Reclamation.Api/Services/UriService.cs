using Microsoft.AspNetCore.WebUtilities;
using Reclamation.Application.Contracts.Specs;

namespace Reclamation.Api.Services;
public class UriService : IUriService
{
    private readonly string _baseUri;
    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }
    public Uri GetPageUri(SpecParams filter, string route)
    {
        var _enpointUri = new Uri(string.Concat(_baseUri, route));
        var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "PageIndex", filter.PageIndex.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}
