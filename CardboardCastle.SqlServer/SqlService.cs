using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CardboardCastle.SqlServer
{
    using Models;
    using Models.Types;

    public interface ISqlService
    {
        int RunScript(string script);

        Task<SqlResponse> RegisterUser(User user);
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
    }
}
