using Newtonsoft.Json.Linq;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Interface
{
    public interface ICrudService<T, K>
    {
        Output Insert(T Model);
        Output Update(T Model);
        Output Delete(K Key);
        Output GetDataBy(K Key);
        Output GetList(JObject JModel);        
    }
}
