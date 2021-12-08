using People.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People.API.Data
{
    public interface IDataAccess
    {
        Task<PersonModel> AddPeople(PersonModel people);
        Task DelPeople(int Id);
        Task<List<PersonModel>> GetAllPeople();
        Task<PersonModel> GetPeopleById(int id);
        Task<PersonModel> UpdPeople(PersonModel people);
    }
}