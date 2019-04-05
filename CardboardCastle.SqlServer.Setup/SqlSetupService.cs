using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardboardCastle.SqlServer.Setup
{
    public interface ISqlSetupService
    {
        void ProcessScripts();
        string[] FilesFromPath(string filepath);
        void ProcessScriptFile(string filepath);
        string SanitizePath(string path);
    }

    public class SqlSetupService : ISqlSetupService
    {
        private readonly IConfiguration configuration;
        private readonly ISqlService sqlService;
        private readonly ILogger logger;

        public SqlSetupService(IConfiguration configuration, ISqlService sqlService, ILogger logger)
        {
            this.configuration = configuration;
            this.sqlService = sqlService;
            this.logger = logger;
        }

        public void ProcessScripts()
        {
            try
            {
                var paths = configuration.GetSection("Database:SetupScripts").Get<string[]>();
                
                foreach(var path in paths)
                {
                    logger.LogDebug($"Processing scripts in \"{path}\"");
                    var files = FilesFromPath(SanitizePath(path));
                    logger.LogDebug($"{files.Length} files found");
                    foreach(var file in files)
                    {
                        ProcessScriptFile(file);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while running SQL Setup");
            }
        }

        public string[] FilesFromPath(string filepath)
        {
            if (!filepath.Contains("*"))
            {
                if (File.Exists(filepath))
                    return new [] { filepath };

                return new string[0];
            }

            return Directory.GetFiles(filepath.Replace("*", ""), "*.sql", SearchOption.AllDirectories);
        }

        public void ProcessScriptFile(string filepath)
        {
            try
            {
                var script = File.ReadAllText(filepath);
                var resp = sqlService.RunScript(script);
                logger.LogDebug($"Script file processed: \"{filepath}\" completed with {resp}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error processing script file: {filepath}");
            }
        }

        public string SanitizePath(string path)
        {
            var parts = path.Split(new []{ "\\", "/"}, StringSplitOptions.None);
            return Path.Combine(parts);
        }
    }
}
