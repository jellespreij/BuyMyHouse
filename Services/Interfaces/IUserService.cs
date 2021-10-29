using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        void UpdateAllMortgages();
        User getSingleUser(Guid id);
    }
}
