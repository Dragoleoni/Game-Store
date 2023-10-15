namespace EfCoreLab.Interfaces;

internal interface IRepository<T>
    where T : class
{
    public List<T> GetAll();
    public T Create(T entity);
    public T Update(T entity);
    public void Delete(T entity);
}
