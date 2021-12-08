using People.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.API.Data
{
    public class DataAccess : IDataAccess
    {
        #region DataList

        //TODO: Change to Real DB

        private List<PersonModel> tbPeople = new List<PersonModel> {
              new PersonModel { Id = 1, Name="Mario", LastName ="Gomez",Email="mariogomez56@gmail.com"},
              new PersonModel { Id = 2, Name = "Jose", LastName = "Robles", Email = "joserobles09@gmail.com" }
            };

        #endregion

        #region Methods

        public Task<List<PersonModel>> GetAllPeople()
        {
            return Task.FromResult(tbPeople);
        }

        public Task<PersonModel> GetPeopleById(int id)
        {
            PeopleExist(id);

            var person = tbPeople.Where(w => w.Id == id).FirstOrDefault();

            return Task.FromResult(person);
        }

        public Task<PersonModel> AddPeople(PersonModel people)
        {
            // Get and set Id
            var id = tbPeople.Max(s => s.Id) + 1;
            people.Id = id;
            tbPeople.Add(people);
            return Task.FromResult(people);
        }

        public Task<PersonModel> UpdPeople(PersonModel people)
        {
            var Id = people.Id;

            PeopleExist(Id);

            var pIndex = tbPeople.FindIndex(w => w.Id == Id);
            tbPeople[pIndex] = people;
            return Task.FromResult(people);
        }

        public Task DelPeople(int Id)
        {
            PeopleExist(Id);

            tbPeople.RemoveAll(x => x.Id == Id);
            return Task.FromResult(true);
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
