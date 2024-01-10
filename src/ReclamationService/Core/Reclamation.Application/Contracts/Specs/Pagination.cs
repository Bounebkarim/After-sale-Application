namespace Reclamation.Application.Contracts.Specs;
public class Pagination<T> where T : class
{
    public Pagination(int pageIndex, int pageSize, int totalRecords, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        Data = data;
    }

    public Pagination()
    {
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Uri FirstPage { get; set; }
    public Uri LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri NextPage { get; set; }
    public Uri PreviousPage { get; set; }
    public IReadOnlyList<T>? Data { get; set; }
}