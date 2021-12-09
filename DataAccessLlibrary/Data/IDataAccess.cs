using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IDataAccess<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T people);
        Task<T> Update(T people);
        Task Delete(int Id);
    }
}