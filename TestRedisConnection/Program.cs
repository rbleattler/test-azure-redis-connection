using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using static System.Text.RegularExpressions.Regex;

namespace TestRedisConnection
{
  class Program
  {
    #region Properties and Fields

    private static string _ourNode;
    private static string _primaryAccessKey;
    private static string _endpointString;
    private static bool _connectionSucceeded;
    private static string _endUrl = "redis.cache.windows.net";
    private static string _port = "6380";
    private static bool _useSsl = true;
    private const string _fqdnPattern = @"^([a-zA-Z0-9][a-zA-Z0-9-]{0,61}[a-zA-Z0-9]\.)+[a-zA-Z]{2,}$";
    private const string _portPattern = @"^\d*$";
    private const string _boolPattern = @"^true|false$";

    private static ConnectionMultiplexer _muxer;

    #endregion

    static async Task Main(string[] args)
    {
      SetVars(args);
      await InvokeRedisConnection();
    }

    private static async Task InvokeRedisConnection()
    {
      var config = new ConfigurationOptions
      {
        EndPoints = { _endpointString },
        Password = _primaryAccessKey,
        Ssl = _useSsl
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

    private static void SetVars(string[] args)
    {
      if (args.Length > 2)
      {
        foreach (var arg in args.Skip(2))
        {
          bool argIsPort = IsMatch(arg, _portPattern);
          bool argIsFqdn = IsMatch(arg, _fqdnPattern);
          bool argIsUseSsl = IsMatch(arg, _boolPattern);

          if (args.Length > 2 && argIsFqdn)
          {
            Console.WriteLine("Setting url to " + arg);
            _endUrl = arg;
          }

          if (args.Length > 2 && argIsPort)
          {
            Console.WriteLine("Using port 6380");
            _port = arg;
          }

          if (args.Length > 2 && argIsUseSsl)
          {
            Console.WriteLine("SSL :" + arg);
            _useSsl = bool.Parse(arg);
          }
        }
      }

      _ourNode = args[0];
      _primaryAccessKey = args[1];
      _endpointString = $"{_ourNode}.{_endUrl}:{_port}";
    }
  }
}