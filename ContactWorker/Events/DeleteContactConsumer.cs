using Application.Services;
using Application.ViewModels.Contact;
using Common.QueueMessageModels;
using ContactWorker.Config;
using MassTransit;

namespace ContactWorker.Events;

public class DeleteContactConsumer: IConsumer<RemoveContactMessage>
{
    private readonly IContactService _contactService;
    private readonly IElasticClient _elasticClient;

    public DeleteContactConsumer(IContactService contactService, IElasticClient elasticClient)
    {
        _contactService = contactService;
        _elasticClient = elasticClient;
    }
    
    public async Task Consume(ConsumeContext<RemoveContactMessage> context)
    {
        var id = context.Message.contactId;

        await _contactService.Delete(id);
        
        var log = new LogMessage(context.Message, id);

        await _elasticClient.PostLog(log, "contact-worker");

        await Task.CompletedTask;
    }
}