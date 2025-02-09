using Application.Services;
using Application.ViewModels.Contact;
using Common.QueueMessageModels;
using ContactWorker.Config;
using Domain.Entities;
using MassTransit;

namespace ContactWorker.Events;

public class CreateContactConsumer: IConsumer<CreateContactMessage>
{
    private readonly IContactService _contactService;
    private readonly IElasticClient _elasticClient;

    public CreateContactConsumer(IContactService contactService, IElasticClient elasticClient)
    {
        _contactService = contactService;
        _elasticClient = elasticClient;
    }

    public async Task Consume(ConsumeContext<CreateContactMessage> context)
    {
        var entity = MapMessageToEntity(context.Message);

        await _contactService.Create(entity);

        var log = new LogMessage(context.Message);

        await _elasticClient.PostLog(log, "contact-worker");

        await Task.CompletedTask;
    }

    private ContactRequestModel MapMessageToEntity(CreateContactMessage message)
    {
        var contact = new ContactRequestModel
        {
            Name = message.Name,
            Email = message.Email,
            Phone = message.Phone,
            Ddd = message.Ddd,
        };
        
        return contact;
    }
}