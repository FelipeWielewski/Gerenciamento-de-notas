using Microsoft.EntityFrameworkCore;
using NeoMode.Core;
using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.User.ToList();
        }
        public User GetById(int Id)
        {
            return _dbContext.User.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }
        public User GetByUsername(string username)
        {
            return _dbContext.User.Where(X => X.Username == username).ToList().FirstOrDefault();
        }

        public void InsertUser(User UserToInsert)
        {
            var newUser = new User();

            newUser.FullName = UserToInsert.FullName;
            newUser.Password = UserToInsert.Password;
            newUser.Username = UserToInsert.Username;
            newUser.Id = 0;

            //Salvando
            _dbContext.User.Add(newUser);

            _dbContext.SaveChanges();
        }
        public bool UpdateUser(User UserToUpdate)
        {
            try
            {
                var old = GetById(UserToUpdate.Id);
                old.FullName = UserToUpdate.FullName;
                old.Password = UserToUpdate.Password;
                old.Username = UserToUpdate.Username;

                _dbContext.Entry(old).State = EntityState.Modified;

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
