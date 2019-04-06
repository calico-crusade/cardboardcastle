using CardboardBox.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardboardCastle.Core
{
    using ApiModels;
    using Models;
    using Models.Types;
    using SqlServer;

    public interface ICastleService
    {
        Task<SqlResponse> Register(User user);
        Task<User> Login(LoginUser user);
        Task<DetailedUser> GetUser(int userid);
        Task<SqlResponse> UpdateProfile(ProfileUpdateUser user, int actorId);
        string PasswordHash(string password, string salt);
        string PasswordHash(string password, string salt, string key);
        string PasswordKey();
        string CreateSalt(int length = 512);
        string CreateToken(User user);
    }

    public class CastleService : ICastleService
    {
        private static readonly Encoding Encoder = Encoding.ASCII;
        private readonly ICoreConfig config;
        private readonly ILogger logger;
        private readonly ISqlService sqlService;
        private readonly IConfiguration settings;

        public CastleService(ICoreConfig config, ILogger logger, ISqlService sqlService, IConfiguration settings)
        {
            this.config = config;
            this.logger = logger;
            this.sqlService = sqlService;
            this.settings = settings;
        }

        public async Task<SqlResponse> Register(User user)
        {
            try
            {
                user.Salt = CreateSalt();
                user.Password = PasswordHash(user.Password, user.Salt);
                return await sqlService.RegisterUser(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during registeration");
                return new SqlResponse
                {
                    Code = QueryResponseCode.Error,
                    Id = 0
                };
            }
        }

        public async Task<User> Login(LoginUser user)
        {
            var prof = await sqlService.FetchUser(user.EmailAddress);
            if (prof == null)
            {
                logger.LogInformation($"Account not found {user.EmailAddress}");
                return null;
            }
            var password = PasswordHash(user.Password, prof.Salt);

            if (password != prof.Password)
            {
                logger.LogInformation($"Password mismatch {user.EmailAddress}");
                return null;
            }

            return prof;
        }

        public async Task<DetailedUser> GetUser(int userid)
        {
            var user = await sqlService.GetUser(userid);
            return user == null ? null : (DetailedUser)user;
        }

        public async Task<SqlResponse> UpdateProfile(ProfileUpdateUser user, int actorId)
        {
            try
            {
                var prof = await sqlService.GetUser(actorId);
                if (prof == null)
                    return SqlResponse.NotFound;

                var password = PasswordHash(user.Password, prof.Salt);
                if (password != prof.Password)
                    return SqlResponse.Unauthorized;

                prof.FirstName = Coalesce(user.FirstName, prof.FirstName, "User");
                prof.LastName = Coalesce(user.LastName, prof.LastName, "name");
                prof.EmailAddress = Coalesce(user.Email, prof.EmailAddress, "example@example.com");

                return await sqlService.UpdateProfile(prof);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during profile update.");
                return SqlResponse.Error;
            }
        }

        public string CreateToken(User user)
        {
            return new JwtToken(settings["Tokens:Key"])
                .SetIssuer(settings["Tokens:Issuer"])
                .SetAudience(settings["Tokens:Issuer"])
                .SetEmail(user.EmailAddress)
                .Expires(int.Parse(settings["Tokens:ExpirationOffset"]))
                .AddClaim(ClaimTypes.Actor, user.UserId.ToString())
                .Write();
        }

        public string PasswordKey()
        {
            if (!File.Exists(config.KeyPath))
            {
                logger.LogWarning($"Password Key file does not exist @ \"{config.KeyPath}\"... Creating.");
                var key = CreateSalt();
                File.WriteAllText(config.KeyPath, key);
                return key;
            }

            return File.ReadAllText(config.KeyPath);
        }

        public string CreateSalt(int length = 512)
        {
            var bytes = new byte[512];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public string PasswordHash(string password, string salt)
        {
            var key = PasswordKey();
            return PasswordHash(password, salt, key);
        }

        public string PasswordHash(string password, string salt, string key)
        {
            byte[] input = Encoder.GetBytes(salt + password + salt),
                   bykey = Encoder.GetBytes(key);

            using (var cat = new HMACSHA512(bykey))
            {
                var hash = cat.ComputeHash(input);
                return hash.Aggregate("", (s, e) => s + string.Format("{0:x2}", e), s => s);
            }
        }

        public string Coalesce(params string[] inputs)
        {
            return inputs.FirstOrDefault(t => !string.IsNullOrEmpty(t));
        }
    }
}
