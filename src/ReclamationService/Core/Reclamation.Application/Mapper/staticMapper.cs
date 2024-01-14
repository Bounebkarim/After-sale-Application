using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Reclamation.Application.Mapper;
public static class staticMapper
{
    public static readonly Lazy<IMapper> Lazy = new(() =>
    {
        var config = new MapperConfiguration((cfg) =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddGlobalIgnore("Id");
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    public static IMapper Mapper => Lazy.Value;
}
