using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class DataAccessMemory : IDataAccess<PersonModel>
    {
        #region DataList

        //TODO: Change to Real DB

        private  readonly List<PersonModel> tbPeople = new ()
        {
            new PersonModel { Id = 1, FirstName="Mario", LastName ="Gomez",Email="mariogomez56@gmail.com"},
            new PersonModel { Id = 2, FirstName = "Jose", LastName = "Robles", Email = "joserobles09@gmail.com" }
        };

        #endregion

        #region Methods

        public async Task<IEnumerable<PersonModel>> GetAll() => await Task.FromResult(tbPeople);

        public async Task<PersonModel> GetById(int id)
        {
            PeopleExist(id);

            var person = tbPeople.Where(w => w.Id == id).FirstOrDefault();

            return await Task.FromResult(person);
        }

        public async Task<PersonModel> Insert(PersonModel people)
        {
            // Get and set Id
            var id = tbPeople.Max(s => s.Id) + 1;
            people.Id = id;
            tbPeople.Add(people);
            return await Task .FromResult(people);
        }

        public async Task<PersonModel> Update(PersonModel people)
        {
            var Id = people.Id;
            PeopleExist(Id);
            var pIndex = tbPeople.FindIndex(w => w.Id == Id);
            tbPeople[pIndex] = people;
            return await Task.FromResult(people);
        }

        public async Task Delete(int Id)
        {
            PeopleExist(Id);
            tbPeople.RemoveAll(x => x.Id == Id);  
            await Task.CompletedTask;
        }

        #endregion

        #region Auxiliar

        private void PeopleExist(int Id)
        {
            var pIndex = tbPeople.FindIndex(w => w.Id == Id);
            if (pIndex == -1)
            {
                //TODO: log error here

                throw new ArgumentException($"Not found people with id { Id}");
            }
        }
        #endregion

    }
}
