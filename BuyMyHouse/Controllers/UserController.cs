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
    class UserController
    {
        public ILogger Logger { get; }
        IUserService UserService { get; }

        public UserController(ILogger<UserController> Logger, IUserService userService)
        {
            this.Logger = Logger;
            UserService = userService;
        }

        [Function("CreateUser")]
        [OpenApiOperation(operationId: "CreateUser", tags: new[] { "user" }, Summary = "Creates a new user", Description = "Creates a new user", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "New user that was added")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(User), Summary = "New user created", Description = "New user created")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Invalid input", Description = "Invalid input")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Conflict, Summary = "User already exists", Description = "User already exists")]
        public async Task<HttpResponseData> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "users")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                User user = JsonConvert.DeserializeObject<User>(requestBody);

                User addedUser = UserService.CreateUser(user).Result;

                if (addedUser is null)
                {
                    response = req.CreateResponse(HttpStatusCode.Conflict);
                }
                else
                {
                    response = req.CreateResponse(HttpStatusCode.Created);
                    await response.WriteAsJsonAsync(addedUser);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Invalid input", ex);
                response = req.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }
    }
}
