using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            //ConfigurationManager.AppSettings["DefaultConnection"]
        }
        public DbSet<Entities> Entities { get; set; }

        public DbSet<Person> People { get; set; }
        //public DbSet<What> What { get; set; }
    }
}
