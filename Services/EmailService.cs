using Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private IUserRepository _userRepository;
        private IMortgageRepository _mortgageRepository;

        public EmailService(IUserRepository userRepository, IMortgageRepository mortgageRepository)
        {
            _userRepository = userRepository;
            _mortgageRepository = mortgageRepository;
        }

        public async Task<Response> Mail(User user)
        {
            Mortgage mortgage = _mortgageRepository.GetMortgageByUserId(user.Id);

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("622120@student.inholland.nl");
            var subject = "BuyMyHouse calculated mortgage";
            var to = new EmailAddress(user.Email);
            var plainTextContent = "Beste " + user.Name + ", het maximale bedrag die u kan lenen is berekent. Volg de link om het bedrag in te zien. Deze link is 12 uur geldig.";
            var htmlContent = "Beste " + user.Name + ", </n>het maximale bedrag die u kan lenen is berekent. Volg de link om het bedrag in te zien. Deze link is 12 uur geldig.</n> <a href='http://localhost:7071/api/mortgage?mortgageId=" + mortgage.Id
                + "'>Klick Here</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }

        public async void MailToAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();

            foreach (User user in users)
            {
                await Mail(user);
            }
        }
    }
}
