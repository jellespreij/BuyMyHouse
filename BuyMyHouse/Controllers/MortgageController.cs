using Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    class MortgageController
    {
        public ILogger Logger { get; }
        IMortgageService MortgageService { get; }

        public MortgageController(ILogger<MortgageController> Logger, IMortgageService mortgageService)
        {
            this.Logger = Logger;
            MortgageService = mortgageService;
        }

        [Function("FindMortgageById")]
        [OpenApiOperation(operationId: "FindMortgageById", tags: new[] { "mortgage" }, Summary = "Find mortgage by mortgageId", Description = "Returns the mortgage of a user by mortgageId.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "mortgageId", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Summary = "Id of mortgage to return", Description = "Id of mortgage to return", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Mortgage), Summary = "successful operation", Description = "successful operation")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid Id supplied", Description = "Invalid Id supplied")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Mortgage not found", Description = "Mortgage not found")]
        public async Task<HttpResponseData> FindMortgageByUserId(
           [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "mortgage")]
           HttpRequestData req,
           String mortgageId,
           FunctionContext executionContext)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            Mortgage mortgage = MortgageService.getSingleMortgage(Guid.Parse(mortgageId));

            if (mortgage is not null)
            {
                DateTime checkTimer = mortgage.WatchableTime;

                if (checkTimer.AddHours(12) > DateTime.Now)
                {
                    await response.WriteAsJsonAsync("Maximaal te lenen bedrag: " + mortgage.CalculatedMortgage.ToString());
                }
                else 
                {
                    await response.WriteAsJsonAsync("Outside of the time window");
                }
            }
            else
            {
                response = req.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}
