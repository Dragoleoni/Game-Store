using GameStoreLab.Domain.Models;

namespace GameStoreLab.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        public List<T> GetCachedList();
        public List<Game> SearchByColumn(string columnName, string searchValue);
    }
}
