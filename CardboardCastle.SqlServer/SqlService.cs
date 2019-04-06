using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CardboardCastle.SqlServer
{
    using Models;
    using Models.Types;

    public interface ISqlService
    {
        int RunScript(string script);
        Task<SqlResponse> RegisterUser(User user);
        Task<SqlResponse> UpdateProfile(User user);
        Task<User> FetchUser(string email);
        Task<User> GetUser(int id);
    }

    public class SqlService : ISqlService
    {
        private readonly IDatabaseConfig config;
        private readonly ILogger logger;

        private SqlConnection Connection => new SqlConnection(config.Connection);

        public SqlService(IDatabaseConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public int RunScript(string script)
        {
            try
            {
                var server = new Server(new ServerConnection(Connection));
                return server.ConnectionContext.ExecuteNonQuery(script);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occurred during script run. Script:\r\n{script}");
                return -1;
            }
        }

        public Task<SqlResponse> RegisterUser(User user)
        {
            return ExecuteStoredProc("[RegisterUser]", new
            {
                user.FirstName,
                user.LastName,
                user.EmailAddress,
                user.Password,
                user.Salt
            }); 
        }

        public async Task<User> FetchUser(string email)
        {
            var users = await QueryStoredProc<User>("[FetchUser]", new { Email = email });

            if (users.Data.Length <= 0)
                return null;

            if (users.Data.Length > 1)
                logger.LogWarning($"More than one user detected for {email}");

            return users.Data.First();
        }

        public async Task<User> GetUser(int id)
        {
            var users = await QueryStoredProc<User>("[GetUser]", new { UserId = id });

            if (users.Data.Length <= 0)
                return null;

            if (users.Data.Length > 1)
                logger.LogWarning($"More than one user detected for {id}");

            return users.Data.First();
        }

        public Task<SqlResponse> UpdateProfile(User user)
        {
            return ExecuteStoredProc("[UpdateProfileUser]", new
            {
                user.FirstName,
                user.LastName,
                user.EmailAddress,
                user.UserId
            });
        }

        public async Task<SqlResponse> ExecuteStoredProc(string proc, object item, string catalog = null)
        {
            using (var con = Connection)
            {
                con.Open();
                var param = new DynamicParameters(item);
                param.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await con.ExecuteAsync
                (
                    sql: (catalog ?? config.Catalog) + proc,
                    param: param,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: config.Timeout
                );

                var code = param.Get<int>("@ret");

                if (code < 0)
                {
                    var queryCode = (QueryResponseCode)Math.Abs(code);
                    return new SqlResponse
                    {
                        Code = queryCode,
                        Id = 0
                    };
                }

                return new SqlResponse
                {
                    Code = QueryResponseCode.Ok,
                    Id = code
                };
            }
        }

        public async Task<SqlResponse<T>> QueryStoredProc<T>(string proc, object item, string catalog = null)
        {
            using (var con = Connection)
            {
                con.Open();
                var param = new DynamicParameters(item);
                param.Add("@ret", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var data = await con.QueryAsync<T>
                (
                    sql: (catalog ?? config.Catalog) + proc,
                    param: param,
                    commandTimeout: config.Timeout,
                    commandType: CommandType.StoredProcedure
                );

                var code = (QueryResponseCode)Math.Abs(param.Get<int>("@ret"));
                return new SqlResponse<T>
                {
                    Code = code,
                    Data = data.ToArray()
                };
            }
        }
    }
}
