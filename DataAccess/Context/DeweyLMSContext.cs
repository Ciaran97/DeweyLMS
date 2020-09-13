using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class DeweyLMSContext : DbContext
    {

        public DeweyLMSContext() : base("DeweyLMSContext")
        {

        }

        public DeweyLMSContext(String ConnectionString) : base(ConnectionString)
        {
            Database.SetInitializer<DeweyLMSContext>(null);
        }

        public DbSet<DeweyClass> DeweyClasses {get; set;}

    }
}
