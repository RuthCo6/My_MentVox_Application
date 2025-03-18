using MentVox.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentVox.Core.Repositories
{
    public class IUserRepository
    {
        User GetUserById(int id);
        void AddUser(User user);

    }
}
