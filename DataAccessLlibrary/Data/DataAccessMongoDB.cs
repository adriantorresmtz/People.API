using DataAccess.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DataAccessMongoDB: IDataAccess<PersonModel>
    {
        #region Constructor

        private readonly IMongoCollection<PersonModel> _db;

        public DataAccessMongoDB(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.DataBase);
            _db = database.GetCollection<PersonModel>(settings.Collection);
        }
        #endregion


        public async Task<IEnumerable<PersonModel>> GetAll()
        {
            return await _db.Find(d => true).ToListAsync();

        }

        public async Task<PersonModel> GetById(int id)
        {
            return await _db.Find(x => x.Id == id).SingleAsync();
        }

        public async Task<PersonModel> Insert(PersonModel people)
        {
            people.Id = await GetMaxId();
            await _db.InsertOneAsync(people);
            return people;
        }

        public async Task<PersonModel> Update(PersonModel people)
        {
            var Id = people.Id;
            await _db.ReplaceOneAsync(w => w.Id == Id, people);

            return people;
        }

        public async Task Delete(int Id)
        {
            await _db.DeleteOneAsync(w => w.Id == Id);
        }

        #region Auxiliar
        private async Task<int> GetMaxId()
        {
            var options = new FindOptions<PersonModel, PersonModel>
            {
                Limit = 1,
                Sort = Builders<PersonModel>.Sort.Descending(o => o.Id)
            };
            var max = (await _db.FindAsync(FilterDefinition<PersonModel>.Empty, options)).FirstOrDefault();

            if (max == null)
            {
                return 1;
            }

            return max.Id + 1;
        }
        #endregion


    }
}
