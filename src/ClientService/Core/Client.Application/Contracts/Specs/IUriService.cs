using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Application.Contracts.Specs;
public interface IUriService
{
    public Uri GetPageUri(SpecParams specParams, string route);
}