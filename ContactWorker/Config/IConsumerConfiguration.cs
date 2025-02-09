namespace ContactWorker.Config;

public interface IConsumerConfiguration
{
    string ContactCreateQueue { get; set; }
    string ContactUpdateQueue { get; set; }
    string ContactDeleteQueue { get; set; }
    string Host { get; set; }
    string Username { get; set; }
    string Password { get; set; }
}