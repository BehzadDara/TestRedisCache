using StackExchange.Redis;
using TestRedisCache;

var redisCache = ConnectionMultiplexer.Connect("localhost").GetDatabase();

foreach (var key in new List<string> { "key1", "key1", "key2" })
{
    var content = redisCache.StringGet(key).ToString();
    if (content is null)
    {
        content = FakeData.Get(key);
        redisCache.SetAdd(key, content);
        Console.WriteLine($"{content} form db");
    }
    else
    {
        Console.WriteLine($"{content} form cache");
    }
}