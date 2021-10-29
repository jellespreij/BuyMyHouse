using System;
using Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;
using Services;

namespace TimerTriggers
{
    public class EmailTimerTrigger
    {
        public ILogger Logger { get; }
        IEmailService EmailService { get; }

        public EmailTimerTrigger(ILogger<EmailTimerTrigger> Logger, IEmailService emailService)
        {
            this.Logger = Logger;
            EmailService = emailService;
        }

        [Function("SendEmails")]
        public void Run([TimerTrigger("0 0 9 * * *")] Timer myTimer, FunctionContext context)
        {
            Logger.LogInformation("Sending emails");
            EmailService.MailToAll();
        }
    }
}

