namespace EstateAgency.Api;

/// <summary>
/// Represents configuration settings for Kafka integration.
/// </summary>
public class KafkaOptions
{
    /// <summary>
    /// Kafka topic to consume messages to.
    /// </summary>
    public string Topic { get; set; } = "kafka-topic";

    /// <summary>
    /// Kafka group id to consume messages to.
    /// </summary>
    public string GroupId { get; set; } = "default-group";

    /// <summary>
    /// Kafka minimal bytes fetch.
    /// </summary>
    public int FetchMinBytes { get; set; } = 1;
}
