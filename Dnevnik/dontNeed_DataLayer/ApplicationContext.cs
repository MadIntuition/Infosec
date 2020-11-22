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
        Database db;
        public ApplicationContext(string fileName) : base()
        {
            //ConfigurationManager.AppSettings["DefaultConnection"]
            db = new Database(fileName);
        }
        public DbSet<Entity> Entities { get; set; }

        
        public DbSet<Document> Documents { get; set; }

        public DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
        
    }
}
