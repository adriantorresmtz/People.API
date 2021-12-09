using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DbAccess
{
    public interface IDataBaseAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storeProcedure, T parameters, string connectionId = "Default");
    }
}