using Newtonsoft.Json.Linq;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Interface
{
    public interface IApiDynamicService<T>
    {
        Output execute(JObject Model);
        DTResultListDyn<dynamic> executeList(JObject Model);
    }
}
