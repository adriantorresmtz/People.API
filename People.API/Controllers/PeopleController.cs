using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController: GenericController<PersonModel>
    {
        #region Constructor

        public PeopleController(IDataAccess<PersonModel> _db) : base(_db) { }
        
        #endregion
    }
}
