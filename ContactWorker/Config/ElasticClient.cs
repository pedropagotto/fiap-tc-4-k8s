using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ContactWorker.Config;

public interface IElasticClient
{
    Task<bool> PostLog<T>(T log, IndexName indexName);
}

public class ElasticClient : IElasticClient
{
    private readonly ElasticsearchClient _client;
    private readonly ILogger<ElasticClient> _logger;
    
    public ElasticClient(IConfiguration config, ILogger<ElasticClient>  logger)
    {
        _client = new ElasticsearchClient(config["elasticsearch:cloudId"], new ApiKey(config["elasticsearch:apiKey"]));
        _logger = logger;
    }
    
    public async Task<bool> PostLog<T>(T log, IndexName indexName)
    {
        try
        {
            var response = await _client.IndexAsync(log, indexName);
            return response.IsValidResponse;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "PostLog exception");
            return false;
        }
    }
}