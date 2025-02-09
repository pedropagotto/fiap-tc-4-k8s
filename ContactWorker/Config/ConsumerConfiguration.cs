namespace ContactWorker.Config;

public class ConsumerConfiguration: IConsumerConfiguration
{
    public string ContactCreateQueue { get; set; }
    public string ContactUpdateQueue { get; set; }
    public string ContactDeleteQueue { get; set; }
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}