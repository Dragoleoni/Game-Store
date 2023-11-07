using GameStoreLab.Domain.Interfaces.Repositories;
using GameStoreLab.Domain.Models;
using GameStoreLab.Infrastructure.Contexts;
using Microsoft.Extensions.Caching.Memory;

namespace GameStoreLab.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private const int EXPIRATION_TIME = 262;

        public BaseRepository(GameStoreContext context,
            IMemoryCache cacheService)
        {
            Context = context;
            CacheService = cacheService;
            Type = typeof(T).Name;
        }

        public GameStoreContext Context { get; set; }
        public IMemoryCache CacheService { get; set; }
        public string Type { get; set; }

        public List<T> GetCachedList()
        {
            CacheService.TryGetValue(Type, out List<T> result);

            if (result == null)
            {
                result = Context.Set<T>().Take(20).ToList();
                CacheService.Set(Type, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(EXPIRATION_TIME)));
            }

            return result;
        }

        public List<Game> SearchByColumn(string column, string searchValue)
        {
            switch (column)
            {
                case "id":
                    return int.TryParse(searchValue, out var result)
                        ? Context.Games.Where(x => x.GameId == result).ToList()
                        : new List<Game>();
                case "title":
                    return Context.Games.Where(x => x.Title.Contains(searchValue)).ToList();
                case "description":
                    return Context.Games.Where(x => x.Description.Contains(searchValue)).ToList();
                default:
                    return new List<Game>();
            }
        }
    }
}
