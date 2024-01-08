using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Application.Contracts.Specs;
public class SpecParams
{
    public SpecParams()
    {
    }
    public SpecParams(int pageIndex, int pageSize)
    {
        this.PageIndex = pageIndex < 1 ? 1 : pageIndex;
        this.PageSize = pageSize > 10 ? 10 : pageSize;
    }
    private const int MaxPageSize = 70;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string? SortBy { get; set; }
    public List<SearchParam>? Search { get; set; }
}