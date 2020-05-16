using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.BLL.Interfaces;
using UsersAndAwards.DAL.Interfaces;
using UsersAndAwards.Entities;

namespace UsersAndAwards.BLL
{
    public class UserLogic : IUserLogic
    {
        private static IUserDao _userDao;

        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void Add(User user)
        {
            _userDao.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _userDao.GetAll();
        }

        public User GetById(int id)
        {
            return _userDao.GetById(id);
        }

        public void RemoveById(int id)
        {
            _userDao.RemoveById(id);
        }
    }
}
