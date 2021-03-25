using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kartaca.API.Models
{
    public class UserManager : IUserService
    {
        private static List<User> _users = new List<User>();
        public void Update(User user)
        {
            var result = _users.Find(r => r.Id == user.Id);
            if (result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
            }
        }

        public void Add(User user)
        {
            user.Id += 1 + _users.Count;
            _users.Add(user);
        }

        public void Remove(User user)
        {
            _users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetById(int userId)
        {
            return _users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
