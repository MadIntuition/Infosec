using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik
{
    public class AuthorizedUsersViewModel : IDisposable
    {
        private ApplicationContext db;
        IEnumerable<AuthorizedUser> usersList;
        public AuthorizedUsersViewModel()
        {
            db = new ApplicationContext();
        }
        public IEnumerable<AuthorizedUser> GetAll()
        {
            return db.AuthorizedUsers;
        }

        public IEnumerable<AuthorizedUser> GetBy(Func<AuthorizedUser, bool> predicate)
        {
            usersList = db.AuthorizedUsers.Where(predicate);
            return usersList;
        }
        public AuthorizedUser GetByLogin(string login)
        {
            return db.AuthorizedUsers.FirstOrDefault(i => i.Login == login);
        }

        public void Create(AuthorizedUser user)
        {
            db.AuthorizedUsers.Add(user);
            db.SaveChanges();
        }

        public void Delete(string login)
        {
            AuthorizedUser book = db.AuthorizedUsers.Find(login);
            if (book != null)
                db.AuthorizedUsers.Remove(book);
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
