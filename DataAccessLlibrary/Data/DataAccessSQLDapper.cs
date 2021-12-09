using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class DataAccessSQLDapper : IDataAccess<PersonModel>
    {
        private readonly IDataBaseAccess _db;

        #region Constructor
        public DataAccessSQLDapper(IDataBaseAccess db)
        {
            this._db = db;
        }
        #endregion

        public async Task<IEnumerable<PersonModel>> GetAll()
        {
            return await _db.LoadData<PersonModel, dynamic>("dbo.spPerson_GetAll", new { });
        }

        public async Task<PersonModel> GetById(int id)
        {
            var results = await _db.LoadData<PersonModel, dynamic>("dbo.spPerson_Get", new { Id = id });

            return results.FirstOrDefault();
        }

        public async Task<PersonModel> Insert(PersonModel people)
        {
            var parameters = new DynamicParameters(); 
            SetPersonParams(people, parameters);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _db.SaveData("dbo.spPerson_Insert", parameters);

            people.Id = (int)parameters.Get<object>("Id");
            return people;
        }

        public async Task<PersonModel> Update(PersonModel people)
        {
            var parameters = new DynamicParameters();
            SetPersonParams(people, parameters);
            parameters.Add("Id", people.Id);

            await _db.SaveData("dbo.spPerson_Update", parameters);

            return people;
        }
        public async Task Delete(int Id)
        {
            await _db.SaveData("dbo.spPerson_Delete", new { Id });
        }

        #region Auxiliar

        private void SetPersonParams(PersonModel people, DynamicParameters parameters)
        {
            parameters.Add("FirstName", people.FirstName);
            parameters.Add("LastName", people.LastName);
            parameters.Add("Email", people.Email);
        }

        #endregion

    }
}
