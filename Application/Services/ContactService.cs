using Application.ViewModels.Contact;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactResponseModel>? GetById(int id)
        {
            var contact = await _contactRepository.GetById(id);

            if (contact is null) return null;

            var contactViewModel = MapToResponseModel(contact);

            return contactViewModel;
        }

        public async Task<List<ContactResponseModel>> FilterByDdd(string Ddd)
        {
            var contactsFiltered = await _contactRepository.FilterByRegion(Ddd);

            var response = new List<ContactResponseModel>();

            foreach (var contact in contactsFiltered)
            {
                var contactViewModel = MapToResponseModel(contact);
                response.Add(contactViewModel);
            }

            return response;
        }

        public async Task<IList<ContactResponseModel>> ListAll()
        {
            var contacts = await _contactRepository.ListAll();

            var response = new List<ContactResponseModel>();

            foreach (var contact in contacts)
            {
                var contactViewModel = MapToResponseModel(contact);
                response.Add(contactViewModel);
            }

            return response;
        }

        public async Task<ContactResponseModel> Create(ContactRequestModel contact)
        {
            var entity = new Contact()
            {
                Name = contact.Name,
                Ddd = contact.Ddd,
                Phone = contact.Phone,
                Email = contact.Email
            };

            var createdEntity = await _contactRepository.Create(entity) ?? throw new Exception("Ocorreu um erro inesperado.");

            var response = MapToResponseModel(createdEntity);

            return response;
        }

        public async Task<ContactResponseModel> Update(int id, ContactRequestModel contactUpdateModel)
        {
            var contatoRetornado = await GetById(id);

            if (contatoRetornado is null)
            {
                return null;
            }

            var entity = MapToEntity(contatoRetornado);

            var entityToUpdate = MapToUpdateEntity(entity, contactUpdateModel);

            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.UtcNow;

            var updatedEntity = await _contactRepository.Update(entityToUpdate);

            var response = MapToResponseModel(updatedEntity);

            return response;
        }

        public async Task<bool> Delete(int id)
        {
            var deleted = await _contactRepository.Delete(id);
            return deleted == 1;
        }

        private static ContactResponseModel MapToResponseModel(Contact contact)
        {
            var contactViewModel = new ContactResponseModel()
            {
                Id = contact.Id,
                CreatedAt = contact.CreatedAt,
                UpdatedAt = contact.UpdatedAt,
                Name = contact.Name,
                Ddd = contact.Ddd,
                Phone = contact.Phone,
                Email = contact.Email
            };
            return contactViewModel;
        }

        private static Contact MapToEntity(ContactResponseModel responseModel)
        {
            var entity = new Contact()
            {
                CreatedAt = responseModel.CreatedAt,
                UpdatedAt = responseModel.UpdatedAt,
                Name = responseModel.Name,
                Ddd = responseModel.Ddd,
                Phone = responseModel.Phone,
                Email = responseModel.Email
            };
            return entity;
        }

        private static Contact MapToUpdateEntity(Contact entity, ContactRequestModel updateModel)
        {
            entity.Name = updateModel.Name;
            entity.Ddd = updateModel.Ddd;
            entity.Phone = updateModel.Phone;
            entity.Email = updateModel.Email;

            return entity;
        }
    }
}
