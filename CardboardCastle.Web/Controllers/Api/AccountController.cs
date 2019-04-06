using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CardboardCastle.Controllers.Api
{
    using Core;
    using Core.ApiModels;
    using Models.Types;

    [ApiController, Produces("application/json"), Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ICastleService castleService;
        private readonly ILogger logger;

        public AccountController(ICastleService castleService, ILogger logger)
        {
            this.castleService = castleService;
            this.logger = logger;
        }

        [Route("api/account/register"), HttpPost, AllowAnonymous]
        [ProducesResponseType(500), ProducesResponseType(200), ProducesResponseType(409), ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]RegisterUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.EmailAddress) ||
                    string.IsNullOrEmpty(user.FirstName) ||
                    string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.Password))
                    return BadRequest();

                logger.LogInformation($"Registration Attempted for {user.FirstName} {user.LastName} ({user.EmailAddress})");

                var resp = await castleService.Register(user);
                if (resp.Code != QueryResponseCode.Ok)
                {
                    logger.LogInformation($"Registration Failed for {user.FirstName} {user.LastName} ({user.EmailAddress}). Reason: {resp.Code}");
                    return StatusCode((int)resp.Code);
                }

                logger.LogInformation($"Registration Success for {user.FirstName} {user.LastName} ({user.EmailAddress}) [{resp.Id}]");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during registration");
                return StatusCode(500);
            }
        }

        [Route("api/account/login"), HttpPost, AllowAnonymous]
        [ProducesResponseType(500), ProducesResponseType(404), ProducesResponseType(200), ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody]LoginUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.EmailAddress) ||
                    string.IsNullOrEmpty(user.Password))
                    return BadRequest();

                logger.LogInformation($"Login attempted for {user.EmailAddress}");

                var resp = await castleService.Login(user);
                if (resp == null)
                {
                    logger.LogInformation($"Login attempted failed, incorrect email or password {user.EmailAddress}");
                    return NotFound();
                }
                var det = (DetailedUser)resp;

                return Ok(new
                {
                    user = det,
                    token = castleService.CreateToken(resp)
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during login");
                return StatusCode(500);
            }
        }

        [Route("api/account/details/{id?}"), HttpGet]
        [ProducesResponseType(200), ProducesResponseType(404), ProducesResponseType(401)]
        public async Task<IActionResult> Details([FromRoute]int? id)
        {
            try
            {
                int strictId = id ?? int.Parse(User.FindFirstValue(ClaimTypes.Actor));
                var user = await castleService.GetUser(strictId);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user profile");
                return StatusCode(500);
            }
        }

        [Route("api/account/update"), HttpPost]
        [ProducesResponseType(200), ProducesResponseType(404), ProducesResponseType(401), ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody]ProfileUpdateUser user)
        {
            //TODO: IMPLEMENT THIS
            return null;
        }
    }
}
