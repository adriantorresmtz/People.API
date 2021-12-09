using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : Controller where T : class
    {
        #region Constructor
        private readonly IDataAccess<T> _db;

        public GenericController(IDataAccess<T> db)
        {
            this._db = db;
        }

        #endregion

        #region Actions

        // GET: api/<T>
        [HttpGet]
        public async Task<IEnumerable<T>> Get() => await _db.GetAll();

        // GET api/<T>/5
        [HttpGet("{id}")]
        public async Task<T> Get(int id) => await _db.GetById(id);

        // POST api/<T>
        [HttpPost]
        public async Task<T> Post([FromBody] T data) => await _db.Insert(data);

        // POST api/<T>/add
        [HttpPost("add")]
        public async Task<T> CreatePerson(T data) => await _db.Insert(data);

        // PUT api/<T>
        [HttpPut]
        public async Task<T> Put([FromBody] T data) => await _db.Update(data);

        // DELETE api/<T>/1
        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _db.Delete(id);

        #endregion

    }
}
