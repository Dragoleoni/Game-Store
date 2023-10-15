using EfCoreLab.Models;

namespace EfCoreLab.Repositories;

internal class DevelopersRepository : BaseRepository<Developer>
{
    public List<Developer> GetAllByFilter()
    {
        return GetAll().Where(x => x.DeveloperId % 10 == 0).ToList();
    }

    public int GetCount() => GetAll().Count();
}
