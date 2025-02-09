using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IList<T>> ListAll();
        Task<T>? GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(int id);
    }
}
