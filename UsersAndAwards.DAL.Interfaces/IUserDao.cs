using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.Entities;

namespace UsersAndAwards.DAL.Interfaces
{
    public interface IUserDao
    {
        void Add(User user);
        void RemoveById(int id);
        User GetById(int id);
        IEnumerable<User> GetAll();
    }
}
