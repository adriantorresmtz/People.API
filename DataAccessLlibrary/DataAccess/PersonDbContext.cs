using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLlibrary.DataAccess
{
    public partial class PersonDbContext : DbContext
    {
        #region Constructor
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        #endregion

        public virtual DbSet<PersonModel> Person { get; set; }

    }
}
