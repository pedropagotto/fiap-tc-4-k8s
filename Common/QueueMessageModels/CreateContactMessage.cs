namespace Common.QueueMessageModels;

public record CreateContactMessage(string Name, string Ddd, string Phone, string Email, string Message);