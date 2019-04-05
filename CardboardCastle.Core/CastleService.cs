using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardboardCastle.Core
{
    using Models;
    using Models.Types;
    using SqlServer;

    public interface ICastleService
    {
        Task<SqlResponse> Register(User user);
        string PasswordHash(string password, string salt);
        string PasswordHash(string password, string salt, string key);
        string PasswordKey();
        string CreateSalt(int length = 512);
    }

    public class CastleService : ICastleService
    {
        private static readonly Encoding Encoder = Encoding.ASCII;
        private readonly ICoreConfig config;
        private readonly ILogger logger;
        private readonly ISqlService sqlService;

        public CastleService(ICoreConfig config, ILogger logger, ISqlService sqlService)
        {
            this.config = config;
            this.logger = logger;
            this.sqlService = sqlService;
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
            return Encoder.GetString(bytes);
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
    }
}
