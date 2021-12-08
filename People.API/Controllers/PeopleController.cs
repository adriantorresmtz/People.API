using Microsoft.AspNetCore.Mvc;
using People.API.Data;
using People.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        #region Constructor
        private readonly IDataAccess _db;

        public PeopleController(IDataAccess db)
        {
            this._db = db;
        }

        #endregion

        //example: http://localhost:5090/api/People

        // GET: api/<People>
        [HttpGet]
        public Task<List<PersonModel>> Get()
        {
            return _db.GetAllPeople();
        }

        // GET api/<People>/5
        [HttpGet("{id}")]
        public Task<PersonModel> Get(int id) => _db.GetPeopleById(id);

        // POST api/<People>
        [HttpPost]
        public Task<PersonModel> Post([FromBody] PersonModel data) => _db.AddPeople(data);

        //example: http://localhost:5090/api/People/add
        // POST api/<People>/add
        [HttpPost("add")]
        public Task<PersonModel> CreatePerson(PersonModel data) => _db.AddPeople(data);

        // PUT api/<People>
        [HttpPut]
        public Task<PersonModel> Put([FromBody] PersonModel data) => _db.UpdPeople(data);

        // DELETE api/<People>/1
        [HttpDelete("{id}")]
        public Task Delete(int id) => _db.DelPeople(id);

    }
}
