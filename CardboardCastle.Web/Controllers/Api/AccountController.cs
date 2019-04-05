using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CardboardCastle.Controllers.Api
{
    using Core;
    using Core.ApiModels;
    using Models.Types;

    [ApiController, Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ICastleService castleService;
        private readonly ILogger logger;

        public AccountController(ICastleService castleService, ILogger logger)
        {
            this.castleService = castleService;
            this.logger = logger;
        }

        [Route("api/account/register"), HttpPost]
        [ProducesResponseType(500), ProducesResponseType(200), ProducesResponseType(409), ProducesResponseType(400)]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.EmailAddress) ||
                    string.IsNullOrEmpty(user.FirstName) ||
                    string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.Password))
                    return StatusCode(400);

                logger.LogInformation($"Registration Attempted for {user.FirstName} {user.LastName} ({user.EmailAddress})");

                var resp = await castleService.Register(user);
                if (resp.Code != QueryResponseCode.Ok)
                {
                    logger.LogInformation($"Registration Failed for {user.FirstName} {user.LastName} ({user.EmailAddress}). Reason: {resp.Code}");
                    return StatusCode((int)resp.Code);
                }

                logger.LogInformation($"Registration Success for {user.FirstName} {user.LastName} ({user.EmailAddress}) [{resp.Id}]");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during registration");
                return StatusCode(500);
            }
        }
    }
}
