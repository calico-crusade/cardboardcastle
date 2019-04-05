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
    public interface ISqlService
    {
        int RunScript(string script);
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


    }
}
