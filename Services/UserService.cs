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
    public class UserService : IUserService
    {
        private const double INTEREST = 1.06;

        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> CreateUser(User user)
        {
            User foundUser = _userRepository.GetSingle(user.Id);
            if (foundUser is null)
            {
                return _userRepository.Add(user);
            }
            else
            {
                return null;
            }
        }

        public User getSingleUser(Guid userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public void UpdateAllMortgages()
        {
            IEnumerable<User> users = _userRepository.GetAll();

            foreach (User user in users)
            {
                //Berekening voor maxiamle lening
                double calculatedMortgage = (user.PurchasePrice / (user.LoanTerm * 12) + (user.PurchasePrice * INTEREST / (user.LoanTerm * 12)));
                double RoundedMortgage = Math.Round(calculatedMortgage, 2, MidpointRounding.AwayFromZero);

                if (user.Mortgage is null)
                {
                    Mortgage mortgage = new() { CalculatedMortgage = RoundedMortgage };

                    user.Mortgage = mortgage;
                }
                else 
                {
                    user.Mortgage.CalculatedMortgage = calculatedMortgage;
                    user.Mortgage.WatchableTime = DateTime.Now;
                }
            }

            _userRepository.Commit();
        }
    }
}
