using System.Reflection;
using AutoMapper;

namespace Intervention.Application.Mapper;
public static class StaticMapper
{
    public static readonly Lazy<IMapper> Lazy = new(() =>
    {
        var config = new MapperConfiguration((cfg) =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    public static IMapper Mapper => Lazy.Value;
}
