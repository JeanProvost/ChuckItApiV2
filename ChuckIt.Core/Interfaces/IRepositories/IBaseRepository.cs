using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task Delete(T model);
    }
}
