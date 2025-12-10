namespace EstateAgency.Kafka;

/// <summary>
/// Represents configuration settings for Kafka integration.
/// </summary>
public class KafkaOptions
{
    /// <summary>
    /// Kafka topic to publish messages to.
    /// </summary>
    public string Topic { get; set; } = "kafka-topic";

    /// <summary>
    /// Delay between produced messages in milliseconds.
    /// </summary>
    public int ProduceDelayMs { get; set; } = 1000;
}
