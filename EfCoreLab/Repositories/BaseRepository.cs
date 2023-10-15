using EfCoreLab.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EfCoreLab.Repositories;

internal abstract class BaseRepository<T> : IRepository<T>
    where T : class
{
    public virtual T Create(T entity)
    {
        using var context = new GameStoreContext();
        context.Set<T>().Add(entity);
        context.SaveChanges();

        return entity;
    }

    public virtual void Delete(T entity)
    {
        using var context = new GameStoreContext();
        context.Set<T>().Remove(entity);
        context.SaveChanges();
    }

    public virtual List<T> GetAll()
    {
        using var context = new GameStoreContext();

        return context.Set<T>().ToList();
    }

    public virtual T Update(T entity)
    {
        using var context = new GameStoreContext();
        context.Set<T>().Update(entity);
        context.SaveChanges();

        return entity;
    }
}
