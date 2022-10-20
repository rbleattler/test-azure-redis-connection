using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace TestRedisConnection
{
    class Program
    {
        #region Properties and Fields

        private static string _ourNode;
        private static string _primaryAccessKey;
        private static string _endpointString;
        private static bool _connectionSucceeded;
        private static string _endUrl = ".redis.cache.windows.net:6380";
        private static ConnectionMultiplexer _muxer;

        #endregion

        static async Task Main(string[] args)
        {
            if (args.Length > 2) _endUrl = args[2];
            _ourNode = args[0];
            _primaryAccessKey = args[1];
            _endpointString = $"{_ourNode}{_endUrl}";
            await ConnectionMethod1();
        }

        private static async Task ConnectionMethod1()
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { _endpointString },
                Password = _primaryAccessKey,
                Ssl = true
            };
            Console.WriteLine(config.ToString().Replace(_primaryAccessKey, "********"));
            try
            {
                _muxer = await ConnectionMultiplexer.ConnectAsync(config);
                _connectionSucceeded = _muxer.IsConnected;
            }
            catch (Exception)
            {
                // We don't do anything with the exception here because we only care if the connection succeeds. 
                _connectionSucceeded = false;
            }

            if (_connectionSucceeded)
            {
                await _muxer.CloseAsync();
                _muxer.Dispose();
            }

            Environment.ExitCode = _connectionSucceeded ? 1 : 0;
            Console.WriteLine($"Connection Succeeded : {_connectionSucceeded}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}