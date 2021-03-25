using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kartaca.API.Models
{
    public interface IUserService
    {
        void Update(User user);
        void Add(User user);
        void Remove(User user);
        List<User> GetAll();
        User GetById(int userId);
    }
}
