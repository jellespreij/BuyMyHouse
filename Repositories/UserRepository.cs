using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CosmosDBContext cosmosDBContext) : base(cosmosDBContext)
        {

        }
    }
}
