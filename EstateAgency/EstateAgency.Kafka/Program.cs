using Confluent.Kafka;
using EstateAgency.Kafka;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOptions<KafkaOptions>()
    .Bind(builder.Configuration.GetSection("Kafka"));

builder.Services.AddHostedService<KafkaProducerWorker>();

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var kafkaConnection = config.GetConnectionString("KafkaConnection") ?? 
        throw new InvalidOperationException("KafkaConnection string is missing");

    var producerConfig = new ProducerConfig
    {
        BootstrapServers = kafkaConnection,
        Acks = Acks.All,
        EnableIdempotence = true
    };

    return new ProducerBuilder<Null, string>(producerConfig).Build();
});

builder.Services.AddSingleton<ApplicationGenerator>();

var host = builder.Build();
host.Run();
