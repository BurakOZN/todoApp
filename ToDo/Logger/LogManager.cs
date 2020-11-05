using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace Logger
{
    public class LogManager : ILogManager
    {
        private Serilog.Core.Logger Logger;
        private readonly IConfiguration _configuration;

        public LogManager(IConfiguration configuration)
        {
            _configuration = configuration;
            Logger = new LoggerConfiguration().WriteTo.Async(
            a => a.File(
                path: _configuration["Serilog:PathInfo"],
                rollingInterval: (RollingInterval)(int.Parse(_configuration["Serilog:RollingInterval"])),
                flushToDiskInterval: TimeSpan.FromMinutes((int.Parse(_configuration["Serilog:FlushToDiskInterval"]))),
                buffered: bool.Parse(_configuration["Serilog:Buffered"])
           )).CreateLogger();
        }
        public void Info(string log)
        {
            Logger.Information(log);
        }
        public void Error(string log)
        {
            Logger.Error(log);
        }
        public void Fatal(string log)
        {
            Logger.Fatal(log);
        }
    }
}
