using Confluent.Kafka;
using EstateAgency.Kafka;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<KafkaProducerWorker>();

var kafkaConnection = builder.Configuration["ConnectionStrings:KafkaConnection"] ?? "localhost:9092";

builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = kafkaConnection,
        Acks = Acks.All,
        EnableIdempotence = true
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddSingleton<ApplicationGenerator>();

var host = builder.Build();
host.Run();
