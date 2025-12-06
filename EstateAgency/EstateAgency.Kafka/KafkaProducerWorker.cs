using Confluent.Kafka;
using System.Text.Json;

namespace EstateAgency.Kafka;

/// <summary>
/// Periodically produces application messages to Kafka.
/// </summary>
/// <param name="logger">Logging service.</param>
/// <param name="generator">Random application generator.</param>
/// <param name="producer">Kafka message producer.</param>
public class KafkaProducerWorker(
    ILogger<KafkaProducerWorker> logger,
    ApplicationGenerator generator,
    IProducer<Null, string> producer,
    IConfiguration configuration) : BackgroundService
{
    /// <summary>
    /// Kafka topic to publish messages to.
    /// </summary>
    private readonly string _topic = configuration["KafkaTopic"] ?? "kafka-topic";

    /// <summary>
    /// Delay between produced messages in milliseconds.
    /// </summary>
    private readonly int _delayMs = int.TryParse(configuration["KafkaProduceDelay"], out var val) ? val : 1000;

    /// <summary>
    /// Produces messages in a loop until cancellation.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("KafkaProducerWorker started. Producing to topic {Topic} every {Delay} ms", _topic, _delayMs);

        while (!stoppingToken.IsCancellationRequested)
        {
            var application = generator.Generate();
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(application)
            };
            try
            {
                var deliveryResult = await producer.ProduceAsync(_topic, message, stoppingToken);
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

            await Task.Delay(_delayMs, stoppingToken);
        }
    }
}
