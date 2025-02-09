using Common.QueueMessageModels;

namespace ContactWorker.Config;

public class LogMessage
{
    public EventId EventId { get; set; }
    public string? Name { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Phone { get; set; } = null;
    public string? Ddd { get; set; } = null;
    public int? ContactId { get; set; } = null;
    public string? Message { get; set; } = null;
    public DateTime LogTime { get; set; } = DateTime.UtcNow;
    public string? HostName { get; set; } = System.Net.Dns.GetHostName();

    public LogMessage(CreateContactMessage createContactMessage)
    {
        EventId = new EventId(1, "CreateContact");
        Name = createContactMessage.Name;
        Email = createContactMessage.Email;
        Phone = createContactMessage.Phone;
        Ddd = createContactMessage.Ddd;
        Message = createContactMessage.Message;
    }
    
    public LogMessage(UpdateContactMessage updateContactMessage, int contactId)
    {
        EventId = new EventId(2, "UpdateContact");
        Name = updateContactMessage.Name;
        Email = updateContactMessage.Email;
        Phone = updateContactMessage.Phone;
        Ddd = updateContactMessage.Ddd;
        ContactId = contactId;
        Message = updateContactMessage.Message;
    }
    
    public LogMessage(RemoveContactMessage removeContactMessage, int contactId)
    {
        EventId = new EventId(3, "RemoveContact");
        Message = removeContactMessage.Message;
        ContactId = contactId;
    }
}   