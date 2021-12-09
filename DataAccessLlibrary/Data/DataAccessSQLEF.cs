using DataAccess.Data;
using DataAccess.Models;
using DataAccessLlibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DataAccessSQLEF : IDataAccess<PersonModel>
    {
        private readonly PersonDbContext _db;

        public DataAccessSQLEF(PersonDbContext db)
        {
            this._db = db;
        }
        public async Task<IEnumerable<PersonModel>> GetAll()
        {
            return await _db.Person.ToListAsync();
        }

        public async Task<PersonModel> GetById(int id)
        {
            return await _db.Person.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PersonModel> Insert(PersonModel people)
        {
            _db.Person.Add(people);
            await _db.SaveChangesAsync();

            return people;
        }

        public async Task<PersonModel> Update(PersonModel people)
        {
            var id = people.Id;
            var entity = await _db.Person.Where(w => w.Id == id).FirstOrDefaultAsync();
            _db.Entry(entity).CurrentValues.SetValues(people);

            return entity;
        }

        public async Task Delete(int Id)
        {
            var entity = await _db.Person.Where(w => w.Id == Id).FirstOrDefaultAsync();
            _db.Person.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
