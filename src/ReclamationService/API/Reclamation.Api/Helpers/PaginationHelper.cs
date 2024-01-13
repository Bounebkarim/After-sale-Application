using Reclamation.Application.Contracts.Specs;

namespace Reclamation.Api.Helpers;

public static class PaginationHelper
{
    public static Pagination<T> CreatePagedReponse<T>(Pagination<T> response, IUriService uriService, string route) where T : class
    {

        var totalPages = ((double)response.TotalRecords / (double)response.PageSize);
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.NextPage =
            response.PageIndex >= 1 && response.PageIndex < roundedTotalPages
                ? uriService.GetPageUri(new SpecParams(response.PageIndex + 1, response.PageSize), route)
                : null;
        response.PreviousPage =
            response.PageIndex - 1 >= 1 && response.PageIndex <= roundedTotalPages
                ? uriService.GetPageUri(new SpecParams(response.PageIndex - 1, response.PageSize), route)
                : null;
        response.FirstPage = uriService.GetPageUri(new SpecParams(1, response.PageSize), route);
        response.LastPage = uriService.GetPageUri(new SpecParams(roundedTotalPages, response.PageSize), route);
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = response.TotalRecords;
        return response;
    }
}
