using Application.Services;
using Application.ViewModels.Contact;
using ApplicationTest.Fixtures;
using Domain.Abstractions;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace ApplicationTest.Services
{
    public class ContactServiceTests
    {
        private readonly ContactService _contactService;
        private readonly Mock<IContactRepository> _mockRepository;

        public ContactServiceTests()
        {
            _mockRepository = new Mock<IContactRepository>();
            _contactService = new ContactService(_mockRepository.Object);
        }

        [Fact(DisplayName = "Deve listar todos contatos")]
        [Trait("Metodo", "ListAll")]
        public async Task ListAll_ShouldReturnAllContacts()
        {
            // Arrange
            var contact1 = ContactFixture.CreateFakeContact();
            contact1.Id = 1;
            var contact2 = ContactFixture.CreateFakeContact();
            contact2.Id = 2;

            var fakeContacts = new List<Contact>()
            {
                contact1, contact2
            };

            _mockRepository.Setup(repo => repo.ListAll()).ReturnsAsync(fakeContacts);

            // Act
            var result = await _contactService.ListAll();

            //Assert
            result.Should().HaveCount(2);
            result.Should().BeOfType<List<ContactResponseModel>>();
        }

        [Fact(DisplayName = "Listar contatos filtrados por DDD")]
        [Trait("Metodo", "FilterByDdd")]
        public async Task FilterByDD_ShouldReturnAllContactsWithReceivedDdd()
        {
            // Arrange
            var contact1 = ContactFixture.CreateFakeContact();
            contact1.Ddd = "11";

            var contact2 = ContactFixture.CreateFakeContact();
            contact2.Ddd = "11";

            var fakeContactsSameDdd = new List<Contact>()
            {
                contact1, contact2
            };

            _mockRepository.Setup(repo => repo.FilterByRegion(contact1.Ddd)).ReturnsAsync(fakeContactsSameDdd);

            // Act
            var result = await _contactService.FilterByDdd(contact1.Ddd);

            //Assert
            result.Should().HaveCount(2);
            result.Should().BeOfType<List<ContactResponseModel>>();
            result[0].Ddd.Equals(contact1.Ddd);
        }

        [Fact(DisplayName = "Busca por id encontrado")]
        [Trait("Metodo", "GetById")]
        public async Task GetContactById_ShouldReturnContact_WhenContactExists()
        {
            // Arrange
            var contact = ContactFixture.CreateFakeContact();
            contact.Id = 1;

            _mockRepository.Setup(repo => repo.GetById(1))!.ReturnsAsync(contact);

            // Act
            var result = await _contactService.GetById(1)!;

            // Assert
            result.Should().BeEquivalentTo(contact);
            result.Id.Should().Be(1);
        }

        [Fact(DisplayName = "Busca por id NAO encontrado")]
        [Trait("Metodo", "GetById")]
        public async Task GetContactById_ShouldReturnNull_WhenContactDoesNotExist()
        {
            // Arrange
            int nonExistentContactId = 999;
            _mockRepository.Setup(repo => repo.GetById(nonExistentContactId)).ReturnsAsync((Contact)null);

            // Act
            var result = await _contactService.GetById(nonExistentContactId);

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Cadastro novo contato")]
        [Trait("Metodo", "Create")]
        public async Task CreateContact_ShouldReturnCreatedContact()
        {
            // Arrange
            var contact = ContactFixture.CreateFakeContact();

            ContactRequestModel contactDto = new();

            _mockRepository.Setup(repo => repo.Create(It.IsAny<Contact>())).ReturnsAsync(contact);

            // Act
            var result = await _contactService.Create(contactDto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(contact.Name);
            result.Email.Should().Be(contact.Email);
            _mockRepository.Verify(repo => repo.Create(It.IsAny<Contact>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizacao contato sucesso")]
        [Trait("Metodo", "Update")]
        public async Task UpdateContact_ShouldReturnUpdatedContact()
        {
            // Arrange
            var contact = ContactFixture.CreateFakeContact();

            ContactRequestModel contactDto = new();

            _mockRepository.Setup(repo => repo.GetById(contact.Id)).ReturnsAsync(contact);
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Contact>())).ReturnsAsync(contact);

            // Act
            var result = await _contactService.Update(contact.Id, contactDto);

            // Assert
            result.Should().BeEquivalentTo(contact);
        }

        [Fact(DisplayName = "Atualizacao contato NAO encontrado")]
        [Trait("Metodo", "Update")]
        public async Task UpdateContact_ShouldReturnNullWhenContactNotFound()
        {
            ContactRequestModel contactDto = new();


            _mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((Contact)null);

            // Act
            var result = await _contactService.Update(99, contactDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Delete contato encontrado")]
        [Trait("Metodo", "Delete")]
        public async Task DeleteContact_ShouldReturnTrue_WhenContactExistsAndIsDeleted()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(1);

            // Act
            var result = await _contactService.Delete(1);

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Delete contato NAO encontrado")]
        [Trait("Metodo", "Delete")]
        public async Task DeleteContact_ShouldReturnFalse_WhenContactDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(0);

            // Act
            var result = await _contactService.Delete(1);

            // Assert
            result.Should().BeFalse();
        }

    }

}

