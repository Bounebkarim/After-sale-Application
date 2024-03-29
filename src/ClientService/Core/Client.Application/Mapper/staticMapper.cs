using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Application.Mapper;
public static class staticMapper
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
