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
    public class AwardLogic : IAwardLogic
    {
        private static IAwardDao _awardDao;

        public AwardLogic(IAwardDao awardLogic)
        {
            _awardDao = awardLogic;
        }

        public void Add(Award award)
        {
            _awardDao.Add(award);
        }

        public IEnumerable<Award> GetAll()
        {
            return _awardDao.GetAll();
        }

        public Award GetById(int id)
        {
            return _awardDao.GetById(id);
        }

        public IEnumerable<Award> GetUserAwards(int idUser)
        {
            return _awardDao.GetUserAwards(idUser);
        }

        public void Remove(int id)
        {
            _awardDao.Remove(id);
        }

        public void Reward(User user, Award award)
        {
            _awardDao.Reward(user, award);
        }

        public void UnReward(User user, Award award)
        {
            _awardDao.UnReward(user, award);
        }
    }
}
