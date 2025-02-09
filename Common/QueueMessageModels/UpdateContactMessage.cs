namespace Common.QueueMessageModels;

public record UpdateContactMessage(int contactId, string Name, string Ddd, string Phone, string Email, string Message);