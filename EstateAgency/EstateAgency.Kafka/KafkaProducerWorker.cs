using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EstateAgency.Kafka;

/// <summary>
/// Periodically produces application messages to Kafka.
/// </summary>
/// <param name="logger">Logging service.</param>
/// <param name="generator">Random application generator.</param>
/// <param name="producer">Kafka message producer.</param>
/// <param name="options">Kafka options params.</param>
public class KafkaProducerWorker(
    ILogger<KafkaProducerWorker> logger,
    ApplicationGenerator generator,
    IProducer<Null, string> producer,
    IOptions<KafkaOptions> options) : BackgroundService
{
    /// <summary>
    /// Contains options for Kafka producer.
    /// </summary>
    private readonly KafkaOptions _options = options.Value;

    /// <summary>
    /// Produces messages in a loop until cancellation.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("KafkaProducerWorker started. Producing to topic {Topic} every {Delay} ms", _options.Topic, _options.ProduceDelayMs);

        while (!stoppingToken.IsCancellationRequested)
        {
            var application = generator.Generate();
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(application)
            };
            try
            {
                var deliveryResult = await producer.ProduceAsync(_options.Topic, message, stoppingToken);
                logger.LogInformation("Produced application to {TopicPartitionOffset}", deliveryResult.TopicPartitionOffset);
            }
            catch (ProduceException<Null, string> ex)
            {
                logger.LogError(ex, "Kafka produce error: {Reason}", ex.Error.Reason);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error producing message");
            }

            await Task.Delay(_options.ProduceDelayMs, stoppingToken);
        }
    }
}
