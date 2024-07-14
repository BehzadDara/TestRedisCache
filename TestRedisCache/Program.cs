using StackExchange.Redis;
using TestRedisCache;

var redisCache = ConnectionMultiplexer.Connect("localhost:6379").GetDatabase();

foreach (var key in new List<string> { "key1", "key1", "key2" })
{
    var content = redisCache.StringGet(key);
    if (string.IsNullOrEmpty(content))
    {
        content = FakeData.Get(key);
        redisCache.StringSet(key, content);
        Console.WriteLine($"{content} from db");
    }
    else
    {
        Console.WriteLine($"{content} from cache");
    }
}

/*

source : https://liara.ir/blog/%D9%86%D8%B5%D8%A8-%D9%88-%D8%B1%D8%A7%D9%87-%D8%A7%D9%86%D8%AF%D8%A7%D8%B2%DB%8C-redis-%D8%AF%D8%B1-%D9%88%DB%8C%D9%86%D8%AF%D9%88%D8%B2/

commands:

wsl --install
curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg
echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list
sudo apt-get update
sudo apt-get install redis
sudo service redis-server start

Start over:
wsl -d ubuntu
sudo systemctl start redis-server

redis-cli -> ping

select 0 (or any number of databases)
keys * (show keys)
GET <key>
SET <key> <value>
*/