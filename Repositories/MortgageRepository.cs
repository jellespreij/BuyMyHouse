using Context;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MortgageRepository : BaseRepository<Mortgage>, IMortgageRepository
    {
        public MortgageRepository(CosmosDBContext cosmosDBContext) : base(cosmosDBContext)
        {

        }

        public Mortgage GetMortgageByUserId(Guid userId)
        {
            return _context.Set<Mortgage>().Where(m => m.UserId == userId).FirstOrDefault();
        }
    }
}
