using MWlaksProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWlaksProject.Core.Interfaces
{
    public interface IGenericInterfaceRepo<T> where T : class
    {
        Task<List<T>> GetAllAsync(QuaryObject quaryObject);

        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> updateAsync(T entity,Guid id);
        Task<T> deleteAsync(Guid id);

    }
    
}
