
namespace Intervention.Application.Contracts.Specs;
public interface IUriService
{
    public Uri GetPageUri(SpecParams specParams, string route);
}