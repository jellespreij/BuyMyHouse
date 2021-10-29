using Interfaces;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MortgageService : IMortgageService
    {
        private IMortgageRepository _mortgageRepository;

        public MortgageService(IMortgageRepository mortgageRepository)
        {
            _mortgageRepository = mortgageRepository;
        }

        public Mortgage getSingleMortgage(Guid mortgageId)
        {
            return _mortgageRepository.GetSingle(mortgageId);
        }
    }
}
