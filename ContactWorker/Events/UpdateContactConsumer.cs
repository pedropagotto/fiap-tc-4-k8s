using Application.Services;
using Application.ViewModels.Contact;
using Common.QueueMessageModels;
using ContactWorker.Config;
using MassTransit;

namespace ContactWorker.Events;

public class UpdateContactConsumer: IConsumer<UpdateContactMessage>
{
    private readonly IContactService _contactService;
    private readonly IElasticClient _elasticClient;

    public UpdateContactConsumer(IContactService contactService, IElasticClient elasticClient)
    {
        _contactService = contactService;
        _elasticClient = elasticClient;
    }

    public async Task Consume(ConsumeContext<UpdateContactMessage> context)
    {
        var id = context.Message.contactId;
        var entity = MapMessageToEntity(context.Message);

        await _contactService.Update(id, entity);
        
        var log = new LogMessage(context.Message, id);

        await _elasticClient.PostLog(log, "contact-worker");

        await Task.CompletedTask;
    }
    
    private ContactRequestModel MapMessageToEntity(UpdateContactMessage message)
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