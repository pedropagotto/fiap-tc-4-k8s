using Application.ViewModels.Contact;
using Bogus;
using Domain.Entities;

namespace ApplicationTest.Fixtures
{
    public static class ContactFixture
    {

        public static Contact CreateFakeContact()
        {
            var contact = new Faker<Contact>()
               .RuleFor(c => c.Id, f => (int)f.Random.UShort())
               .RuleFor(c => c.CreatedAt, f => f.Date.Recent())
               .RuleFor(c => c.Name, f => f.Person.FullName)
               .RuleFor(c => c.Email, f => f.Person.Email)
               .RuleFor(c => c.Ddd, f => f.Random.Number(11, 99).ToString())
               .RuleFor(c => c.Phone, f => string.Concat(f.Random.Number(98000, 99999).ToString(), "-", f.Random.Number(1000, 9999).ToString()))
               .Generate();

            return contact;
        }

        public static ContactRequestModel CreateFakeContactRequestModel()
        {
            var contact = new Faker<ContactRequestModel>()
               .RuleFor(c => c.Name, f => f.Person.FullName)
               .RuleFor(c => c.Email, f => f.Person.Email)
               .RuleFor(c => c.Ddd, f => f.Random.Number(11, 99).ToString())
               .RuleFor(c => c.Phone, f => string.Concat(f.Random.Number(98000, 99999).ToString(), "-", f.Random.Number(1000, 9999).ToString()))
               .Generate();

            return contact;
        }

        public static ContactResponseModel CreateFakeContactResponseModel()
        {
            var contact = new Faker<ContactResponseModel>()
               .RuleFor(c => c.Id, f => (int)f.Random.UShort())
               .RuleFor(c => c.CreatedAt, f => f.Date.Recent())
               .RuleFor(c => c.Name, f => f.Person.FullName)
               .RuleFor(c => c.Email, f => f.Person.Email)
               .RuleFor(c => c.Ddd, f => f.Random.Number(11, 99).ToString())
               .RuleFor(c => c.Phone, f => string.Concat(f.Random.Number(98000, 99999).ToString(), "-", f.Random.Number(1000, 9999).ToString()))
               .Generate();

            return contact;
        }
    }
}
