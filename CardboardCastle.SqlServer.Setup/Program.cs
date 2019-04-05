using CardboardBox.Setup;
using CardboardBox.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;

namespace CardboardCastle.SqlServer.Setup
{
    public interface IProgram
    {
        void Start();
    }

    public class Program : IProgram
    {
        private readonly ILogger logger;
        private readonly ISqlSetupService sqlSetupService;

        public Program(ILogger logger, ISqlSetupService sqlSetupService)
        {
            this.logger = logger;
            this.sqlSetupService = sqlSetupService;
        }

        public void Start()
        {
            logger.LogDebug("Starting script processing");
            sqlSetupService.ProcessScripts();
            logger.LogDebug("Finishing script processing");
        }

        static void Main(string[] args)
        {
            var fp = new PhysicalFileProvider(AppContext.BaseDirectory);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(fp, "appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            var databSettings = config.GetSection("Database").Get<DatabaseConfig>();

            MapHandler.Start()
                      .Use<IDatabaseConfig>(databSettings)
                      .Use<IConfiguration>(config)
                      .AddNLog()
                      .Build<IProgram>()
                      .Start();
        }
    }
}
