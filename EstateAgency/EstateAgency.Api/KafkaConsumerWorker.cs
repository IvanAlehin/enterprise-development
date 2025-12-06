using AutoMapper;
using Confluent.Kafka;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Interfaces;
using System.Text.Json;

namespace EstateAgency.Api;

/// <summary>
/// Kafka background consumer that reads messages and persists applications.
/// </summary>
/// <param name="logger">Logging service.</param>
/// <param name="consumer">Kafka consumer instance.</param>
/// <param name="scopeFactory">Factory for creating service scopes.</param>
/// <param name="mapper">Object mapper.</param>
public class KafkaConsumerWorker(
    ILogger<KafkaConsumerWorker> logger,
    IConsumer<Ignore, string> consumer,
    IServiceScopeFactory scopeFactory,
    IMapper mapper,
    IConfiguration configuration) : BackgroundService
{
    /// <summary>
    /// Kafka topic to listen to.
    /// </summary>
    private readonly string _topic = configuration["KafkaTopic"] ?? "kafka-topic";

    /// <summary>
    /// Consumes messages in a loop and processes them until cancellation.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Subscribe(_topic);

        logger.LogInformation("KafkaConsumerWorker started. Listening topic: {Topic}", _topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = consumer.Consume(stoppingToken);
                if (cr?.Message?.Value == null)
                {
                    logger.LogWarning("Received empty Kafka message");
                    continue;
                }

                var dto = JsonSerializer.Deserialize<ApplicationEditDto>(cr.Message.Value);
                if (dto == null)
                {
                    logger.LogWarning("Failed to deserialize message: {Value}", cr.Message.Value);
                    continue;
                }

                using var scope = scopeFactory.CreateScope();
                var applicationRepo = scope.ServiceProvider
                    .GetRequiredService<IRepository<Domain.Entities.Application>>();

                var entity = mapper.Map<Domain.Entities.Application>(dto);
                var addedEntity = await applicationRepo.AddAsync(entity);
                logger.LogInformation("Saved Application: {@Application}", addedEntity);
                consumer.Commit(cr);
            }
            catch (ConsumeException cex)
            {
                logger.LogError(cex, "Kafka consume error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected consumer error");
            }
        }
    }
}
