using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMortgageRepository : IBaseRepository<Mortgage>
    {
        Mortgage GetMortgageByUserId(Guid userId);
    }
}
