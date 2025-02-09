using Application.ViewModels.Contact;

namespace Application.Services
{
    public interface IContactService
    {
        Task<IList<ContactResponseModel>> ListAll();
        Task<List<ContactResponseModel>> FilterByDdd(string Ddd);
        Task<ContactResponseModel>? GetById(int id);
        Task<ContactResponseModel> Create(ContactRequestModel contact);
        Task<ContactResponseModel> Update(int id, ContactRequestModel contact);
        Task<bool> Delete(int id);
    }
}
