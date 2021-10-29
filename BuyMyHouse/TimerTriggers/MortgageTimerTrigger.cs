using System;
using Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Models;

namespace TimerTriggers
{
    public class MortgageTimerTrigger
    {
        public ILogger Logger { get; }
        IUserService UserService { get; }

        public MortgageTimerTrigger(ILogger<MortgageTimerTrigger> Logger, IUserService userService)
        {
            this.Logger = Logger;
            UserService = userService;
        }

        [Function("CalculateMorgages")]
        public void Run([TimerTrigger("0 0 0 * * *")] Timer myTimer, FunctionContext context)
        {
            Logger.LogInformation("Calculating mortgages");
            UserService.UpdateAllMortgages();
        }
    }
}


