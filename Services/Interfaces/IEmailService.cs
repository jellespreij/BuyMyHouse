using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Interfaces
{
    public interface IEmailService
    {
        public void MailToAll();
        public Task<Response> Mail(User user);
    }
}
