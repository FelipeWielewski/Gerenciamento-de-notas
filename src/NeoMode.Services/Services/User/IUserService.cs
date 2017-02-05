using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int Id);
        User GetByUsername(string username);
        void InsertUser(User UserToInsert);
        bool UpdateUser(User UserToUpdate);
    }
}
