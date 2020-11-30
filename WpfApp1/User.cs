using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1
{
    class User
    {
        public Database Db { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            Db = null;
            Login = login;
            Password = password;
            Name = "-";
            Surname = "-";
            DateOfBirth = new DateTime();
        }

        public void CreateDatabase()
        {
            Db = new Database(Login + ".sqlite");
            Db.CreateFile();
        }
    }
}
