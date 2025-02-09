using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IList<Contact>> FilterByRegion(string Ddd);
    }
}
