using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAndAwards.Entities;

namespace UsersAndAwards.DAL.Interfaces
{
    public interface IAwardDao
    {
        void Add(Award award);
        void Remove(int id);
        Award GetById(int id);
        IEnumerable<Award> GetAll();
        void Reward(User user, Award award);
        void UnReward(User user, Award award);
        IEnumerable<Award> GetUserAwards(int idUser);
    }
}
