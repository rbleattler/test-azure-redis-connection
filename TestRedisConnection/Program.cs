using StackExchange.Redis;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestRedisConnection
{
    class Program
    {
        //Update values
        const string OUR_NODE = "YOUR_NODE_HERE";
        const string PRIMARY_ACCESS_KEY = "YOUR_PRIMARY_KEY_HERE";
        const string REDIS_CONNECTION_STRING = "YOUR_CONNECTION_STRING_HERE";

        const string testKey = "testKey";
        const string testValue = "testValue";

        #region Properties and Fields
        private static Lazy<ConnectionMultiplexer> redisConnection = GetNewRedisConnection();
        public static LazyThreadSafetyMode LazyThreadSafetyMode = LazyThreadSafetyMode.None;
        private static Lazy<ConnectionMultiplexer> GetNewRedisConnection()
        {
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(REDIS_CONNECTION_STRING);
            }, LazyThreadSafetyMode);
        }

        public static IDatabase Cache
        {
            get
            {
                return Connection.GetDatabase();
            }
        }

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return redisConnection.Value;
            }
            set
            {
                if (value == null)
                    redisConnection = GetNewRedisConnection();
            }
        }
        #endregion

        async static Task Main()
        {
            await ConnectionMethod1();
        }

        private static async Task ConnectionMethod1()
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { $"{OUR_NODE}.redis.cache.windows.net" },
                Password = PRIMARY_ACCESS_KEY,
                Ssl = true
            };
            Console.WriteLine(config);

            var muxer = await ConnectionMultiplexer.ConnectAsync(config);
            var db = muxer.GetDatabase();

            RedisKey key = "abc";
            await db.KeyDeleteAsync(key);
            await db.StringSetAsync(key, 42);

            var val = (int)await db.StringGetAsync(key);

            Console.WriteLine(val);
            Console.ReadLine();

        }
    }
}
